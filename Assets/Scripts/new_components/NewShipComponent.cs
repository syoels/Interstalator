using UnityEngine;
using System.Collections;

namespace Interstalator {
public abstract class NewShipComponent : MonoBehaviour {
    public NewShipComponent[] children;
    private NewShipComponentInput[] inputs;
    private NewShipComponentOutput[] outputs;
    private bool _isProcessing;
    public bool isProcessing {
        get {
            foreach (NewShipComponent child in children) {
                if (child.isProcessing) {
                    return true;
                }
            }
            return _isProcessing;
        }
    }

    protected void Awake() {
        // Define inputs and outputs
        outputs = new NewShipComponentOutput[children.Length];
        for (int i = 0; i < children.Length; i++) {
            NewShipComponent child = children[i];
            outputs[i] = new NewShipComponentOutput(child);
        }

        ElementTypes[][] possibleInputTypes = DefineInputs();
        inputs = new NewShipComponentInput[possibleInputTypes.Length];
        for (int i = 0; i < possibleInputTypes.Length; i++) {
            inputs[i] = new NewShipComponentInput(possibleInputTypes[i]);
        }
    }

    // Used by ship component to build the initial inputs list
    virtual protected ElementTypes[][] DefineInputs() {
        return new ElementTypes[0][];
    }

    // Mark inputs as not received to allow running the next flow
    public void ResetInputs() {
        foreach (NewShipComponentInput input in inputs) {
            input.received = false;
        }
    }

    // Insert new inputs to the component
    public int UpdateInput(ElementTypes type, float amount, int index) {
        if (index != -1 && !inputs[index].received) {
            // TODO: Maybe check here if inputs have changed
            inputs[index].type = type;
            inputs[index].amount = amount;
            return index;
        } else {
            // Should only happen in the initial flow
            for (int i = 0; i < inputs.Length; i++) {
                if (inputs[i].IsTypeOK(type) && !inputs[i].received && !inputs[i].assigned) {
                    inputs[i].type = type;
                    inputs[i].amount = amount;
                    inputs[i].assigned = true;
                    // Gives the input index for future updates
                    return i;
                }
            }
        }
        // Shouldn't reach here
        throw new UnityException(
            gameObject.name  + " does not have available input for given type: " + type
        );
    }

    public int GetAwaitingInputs() {
        int waitingInputs = 0;
        foreach (NewShipComponentInput input in inputs) {
            if (!input.received) {
                waitingInputs++;
            }
        }
        return waitingInputs;
    }

    // Main function that holds the logic for each component
    protected abstract NewShipComponentOutput[] InnerProcess();

    // Checks how long we should wait before updating the children
    protected abstract float SetProcessingDelay();

    // Main processing function that processes this component and
    // it's children
    public IEnumerator Process () {
        Debug.Assert(GetAwaitingInputs() == 0);
        yield return new WaitUntil(() => (!isProcessing));
        _isProcessing = true;

        NewShipComponentOutput[] outputs = InnerProcess();
        // TODO: Add delay here

        foreach (NewShipComponentOutput output in outputs) {
            output.Send();
            NewShipComponent child = output.component;
            if (child.GetAwaitingInputs() == 0) {
                StartCoroutine(child.Process());
            }
        }
        _isProcessing = false;
    }

    // These methods are used by Interactable
    abstract public void SetGlow(bool isGlowing);
}
}