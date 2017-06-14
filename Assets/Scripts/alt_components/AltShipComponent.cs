using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// Used to check incoming input
using System.Linq;

namespace Interstalator {
public abstract class AltShipComponent : ShipComponent {
    private bool _isProcessing = false;
    public bool isProcessing {
        get {
            foreach (AltShipComponent child in children) {
                if (_isProcessing) {
                    return true;
                }
            }
            return _isProcessing;
        }
    }

    public IEnumerator StartProcessing(float waitTime) {
        Debug.Assert(GetRemainingIncoming() == 0);
        _isProcessing = true;

        List<Output> outputs = Process();

        // TODO: Handle setting/changing and waiting for state change animation
        yield return new WaitForSeconds(waitTime);

        _isProcessing = false;

        foreach (Output output in outputs) {
            AltShipComponent child = (AltShipComponent)output.component;
            // Deciding on how long to wait should be handled inside StartProcessing
            float animationTime = child.UpdateInput(output.type, output.amount);
            if (child.GetRemainingIncoming() == 0) {
                StartCoroutine(child.StartProcessing(animationTime));
            }
        }
    }

}
}