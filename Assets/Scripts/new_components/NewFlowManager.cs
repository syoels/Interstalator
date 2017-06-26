using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Interstalator {
public class NewFlowManager : MonoBehaviour {

    private bool runningFlow = false;
    private NewShipComponent[] allComponents;
    private List<NewShipComponent> originList = new List<NewShipComponent>();


    void Start() {
        allComponents = FindObjectsOfType<NewShipComponent>();
        Flow();
    }
        
    // Runs the flow and updates the state of every component
    public void Flow() {
        StartCoroutine(FlowRoutine());
    }

    /// <summary>
    /// The main logic of the flow. Runs as a coroutine to allow adding delays
    /// to play animations
    /// </summary>
    protected IEnumerator FlowRoutine() {
        // Avoids running two flows at once
        yield return new WaitUntil(() => !runningFlow);

        runningFlow = true;

        originList = new List<NewShipComponent>();

        foreach (NewShipComponent comp in allComponents) {
            comp.ResetInputs();
            if (comp.isOrigin) {
                originList.Add(comp);
                StartCoroutine(comp.Process());
            }
        }

        yield return new WaitUntil(() => originProcessing() == false);

        runningFlow = false;
    }

    private bool originProcessing() {
        foreach (NewShipComponent ship in originList) {
            if (ship.isProcessing) {
                return true;
            }
        }
        return false;
    }
}
}