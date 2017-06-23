
namespace Interstalator {
public class NewShipComponentOutput {
    private NewShipComponent component;
    private int inputIndex;
    private ElementTypes type;
    private float amount;

    public NewShipComponentOutput(ShipComponent comp, ElementTypes type, float amount, int index=null) {
        this.component = comp; 
        this.type = type; 
        this.amount = amount;
        this.inputIndex = index;
    }

    public void Send(ElementTypes type, float amount) {
        this.type = type;
        this.amount = amount;

        if (this.inputIndex != null) {
            component.UpdateInput(this.type, this.amount, inputIndex);
        } else {
            inputIndex = component.UpdateInput(this.type, this.amount);
        }
    }
}
}