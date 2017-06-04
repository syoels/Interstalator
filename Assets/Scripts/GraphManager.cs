using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public class GraphManager : MonoBehaviour {
    // Seconds to wait after processing each component
    private const float DELAY = 0.5f;
    private bool runningFlow = false;

    // Used to access the graph manager globally
    public static GraphManager instance;
    public ShipStatusController statusController;
    private ItemDisplayController displayController;

    void Awake() {
        instance = this;
        statusController = GetComponent<ShipStatusController>();
        displayController = GetComponent<ItemDisplayController>();
    }

    void Start() {
        statusController.SetProblem(ShipStatusController.ShipSystem.Air, "No air!");
        Flow();
    }

    public void SetInteractionDescription(string interaction) {
        displayController.SetInteraction(interaction);
    }

    public void CancelInteractionDescription() {
        displayController.SetInteraction(null);
    }

    /// <summary>
    /// Runs the flow and updates the state of every component
    /// </summary>
    public void Flow() {
        StartCoroutine(FlowRoutine());
    }

    /// <summary>
    /// The main logic of the flow. Runs as a coroutine to allow adding delays
    /// to play animations
    /// </summary>
    private IEnumerator FlowRoutine() {
        // Avoids running two flows at once
        yield return new WaitUntil(() => !runningFlow);

        runningFlow = true;
        ShipComponent[] allComponents = FindObjectsOfType<ShipComponent>();
        Queue<ShipComponent.Output> queue = new Queue<ShipComponent.Output>(); 

        // Start with origins
        foreach (ShipComponent c in allComponents) {
            c.ResetIncoming();
            if (c.IsOrigin()) {
                ShipComponent.Output originComponent = new ShipComponent.Output(c, ElementTypes.None, 0f);
                queue.Enqueue(originComponent);
            }
        }

        while (queue.Count > 0) {
            yield return new WaitForSeconds(1f);
            ShipComponent.Output transmission = queue.Dequeue();
            ShipComponent curr = transmission.component; 
            curr.UpdateInput(transmission.type, transmission.amount);
            if (curr.GetRemainingIncoming().Count > 0) {
                curr.SetStatus("Waiting for " + curr.GetRemainingIncoming().Count.ToString() + " more inputs");
                queue.Enqueue(transmission);
                continue;
            }

            List<ShipComponent.Output> children = curr.Process();
            foreach (ShipComponent.Output t in children) {
                queue.Enqueue(t);
            }
        }

        runningFlow = false;
    }


}
}
