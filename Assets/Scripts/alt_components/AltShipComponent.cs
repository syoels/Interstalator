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

    public IEnumerator StartProcessing() {
        Debug.Assert(GetRemainingIncoming() == 0);
        _isProcessing = true;

        List<Output> outputs = Process();
        SetAnimationParams();
        yield return new WaitForSeconds(0.1f); //TODO: hacky
        float animationTime = GetRemainingAnimationTime(); 

        // TODO: Handle setting/changing and waiting for state change animation
        yield return new WaitForSeconds(animationTime);

        _isProcessing = false;

        foreach (Output output in outputs) {
            AltShipComponent child = (AltShipComponent)output.component;
            // Deciding on how long to wait should be handled inside StartProcessing

            child.UpdateInput(output.type, output.amount);
            if (child.GetRemainingIncoming() == 0) {
                StartCoroutine(child.StartProcessing());
            }
        }
    }

}
}