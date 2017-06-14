using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Interstalator {
public class AltFlowManager : FlowManager {
    private List<AltShipComponent> originList = new List<AltShipComponent>();

    /// <summary>
    /// The main logic of the flow. Runs as a coroutine to allow adding delays
    /// to play animations
    /// </summary>
    override protected IEnumerator FlowRoutine() {
        Debug.Log("running new flow!");
        // Avoids running two flows at once
        yield return new WaitUntil(() => !runningFlow);

        runningFlow = true;

        originList = new List<AltShipComponent>();

        foreach (AltShipComponent altComp in allComponents) {
            altComp.ResetIncoming();
            if (altComp.IsOrigin()) {
                originList.Add(altComp);
                StartCoroutine(altComp.StartProcessing(0));
            }
        }

        yield return new WaitUntil(() => originProcessing() == false);

        runningFlow = false;
    }

    private bool originProcessing() {
        foreach (AltShipComponent ship in originList) {
            if (ship.isProcessing) {
                return true;
            }
        }
        return false;
    }
}
}