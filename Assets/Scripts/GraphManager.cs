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

    public void Flow() {
        StartCoroutine(FlowRoutine());
    }

    //TODO: change to "void" after textual simulation works properly (IEnumerator is for time delay in simulation)
    private IEnumerator FlowRoutine() {
        yield return new WaitUntil(() => !runningFlow);

        runningFlow = true;
        ShipComponent[] allComponents = FindObjectsOfType<ShipComponent>();
        Queue<ShipComponent.Output> queue = new Queue<ShipComponent.Output>(); 

        // Start with origins
        foreach (ShipComponent c in allComponents) {
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
