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
        Debug.Log(
            string.Format("Sending {0:0.00} {1} to {2}",
                amount,
                type,
                component.name)
        );
        inputIndex = _component.UpdateInput(type, amount, inputIndex);
    }
}
}