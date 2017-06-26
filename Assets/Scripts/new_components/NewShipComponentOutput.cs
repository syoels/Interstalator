using UnityEngine;

namespace Interstalator {
public class NewShipComponentOutput {
    private NewShipComponent _component;
    public NewShipComponent component { get { return _component; } }
    private int inputIndex;
    private ElementTypes type;
    private float amount;

    public NewShipComponentOutput(NewShipComponent comp) {
        this._component = comp; 
        // Dummy value - means the index is not set
        inputIndex = -1;
    }

    public void Set(ElementTypes type, float amount) {
        this.type = type;
        this.amount = amount;
    }

    public void Send() {
        inputIndex = _component.UpdateInput(type, amount, inputIndex);
    }
}
}