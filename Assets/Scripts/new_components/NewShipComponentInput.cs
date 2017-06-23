using UnityEngine;

namespace Interstalator {
public class NewShipComponentInput {
    private ElementTypes[] possibleTypes;
    public ElementTypes type;
    public float amount;
    public bool received;
    public bool assigned;

    public NewShipComponentInput(ElementTypes[] possibleTypes) {
        this.possibleTypes = possibleTypes;
        received = false;
        assigned = false;
    }

    public bool IsTypeOK(ElementTypes type) {
        foreach (ElementTypes possibleType in possibleTypes) {
            if (possibleType == type) {
                return true;
            }
        }
        return false;
    }

    public bool Set(ElementTypes type, float amount) {
        if (!IsTypeOK(type)) {
            throw new UnityException("Got input of wrong type: " + type);
        }
        bool changed = (type != this.type || amount != this.amount);
        this.type = type;
        this.amount = amount;
        return changed;
    }
}
}