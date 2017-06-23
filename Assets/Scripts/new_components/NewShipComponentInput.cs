
namespace Interstalator {
public class NewShipComponentInput {
    private ElementTypes[] possibleTypes;
    public ElementTypes type;
    public float amount;
    public bool received;
    public bool assigned {
        get {
            return type == null;
        }
    }

    public NewShipComponentInput(ElementTypes[] possibleTypes) {
        this.possibleTypes = possibleTypes;
        received = false;
    }

    public bool IsTypeOK(ElementTypes type) {
        foreach (ElementTypes possibleType in possibleTypes) {
            if (possibleType == type) {
                return true;
            }
        }
        return false;
    }
}
}