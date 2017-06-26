using UnityEngine;
using System.Collections;

namespace Interstalator {
public class NewTextualGenericPipe : NewTextualShipComponent {
    public ElementTypes[] possibleTypes;

    protected override ElementTypes[][] DefineInputs() {
        return new ElementTypes[1][] { possibleTypes };
    }

    protected override NewShipComponentOutput[] InnerProcess() {
        float amount = inputs[0].amount;
        ElementTypes type = inputs[0].type;
        SetStatus("Transferring " + amount.ToString("0.00 ") + type);
        return DistributeAmongChildren(type, amount);
    }
}
}