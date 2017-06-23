using UnityEngine;
using System.Collections;

namespace Interstalator {
public abstract class NewShipComponent : MonoBehaviour {

    [SerializeField]private NewShipComponent[] children;
    private NewShipComponentInput[] inputs;
    private NewShipComponentOutput[] outputs;


    public int UpdateInput(ElementTypes type, float amount, int index=null) {
        if (index != null && !inputs[index].received) {
            inputs[index].type = type;
            inputs[index].amount = amount;
        } else {
            // Should only happen in the initial flow
            for (int i = 0; i < inputs.Length; i++) {
                if (inputs[i].IsTypeOK(type) && !inputs[i].received && !inputs[i].assigned) {
                    inputs[i].type = type;
                    inputs[i].amount = amount;
                    // Gives the input index for future updates
                    return i;
                }
            }
        }
        // Shouldn't reach here
        throw new UnityException(
            gameObject.name  + " does not have available input for given type: " + type
        );
        return null;
    }
}
}