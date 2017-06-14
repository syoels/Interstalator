using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Interstalator {
public class FlowManager : MonoBehaviour {

    protected bool runningFlow = false;
    protected ShipComponent[] allComponents;

    void Start() {
        allComponents = FindObjectsOfType<ShipComponent>();
        Flow();
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
    virtual protected IEnumerator FlowRoutine() {
        // Avoids running two flows at once
        yield return new WaitUntil(() => !runningFlow);

        runningFlow = true;
        Queue<ShipComponent.Output> queue = new Queue<ShipComponent.Output>(); 

        // Start with origins
        foreach (ShipComponent c in allComponents) {
            c.ResetIncoming();
            if (c.IsOrigin()) {
                // Creates an empty output that goes to the origins to start the flow process through them
                ShipComponent.Output originComponent = new ShipComponent.Output(c, ElementTypes.None, 0f);
                queue.Enqueue(originComponent);
            }
        }

        while(queue.Count > 0) {
            ShipComponent.Output transmission = queue.Dequeue();
            ShipComponent curr = transmission.component;
            float delay = curr.UpdateInput(transmission.type, transmission.amount);
            yield return new WaitForSeconds(delay);
            int remainingIncoming = curr.GetRemainingIncoming();
            if (remainingIncoming > 0) {
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