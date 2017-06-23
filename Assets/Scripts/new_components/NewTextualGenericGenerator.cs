using UnityEngine;
using System.Collections;

namespace Interstalator {
public class NewTextualGenericGenerator :  NewTextualShipComponent {
    public float amount;
    public ElementTypes type;

    protected override NewShipComponentOutput[] InnerProcess() {
        SetStatus("Sending " + amount.ToString("0.0") + " " + type);
        return DistributeAmongChildren(type, amount);
    }
}
}