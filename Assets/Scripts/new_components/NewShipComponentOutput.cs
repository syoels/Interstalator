using UnityEngine;

namespace Interstalator {
public class NewShipComponentOutput {
    private NewShipComponent _component;
    public NewShipComponent component { get { return _component; } }
    private int inputIndex;
    private ElementTypes type;
    private float amount;

    public NewShipComponentOutput(ShipComponent comp) {
        this._component = comp; 
    }

    public void Set(ElementTypes type, float amount) {
        this.type = type;
        this.amount = amount;
    }

    public void Send() {
        Debug.Assert(type != null && amount != null, "Output parameters not defined", this);
        if (this.inputIndex != null) {
            _component.UpdateInput(this.type, this.amount, inputIndex);
        } else {
            inputIndex = _component.UpdateInput(this.type, this.amount);
        }
    }
}
}