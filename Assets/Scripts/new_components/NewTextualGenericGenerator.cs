using UnityEngine;
using System.Collections;

namespace Interstalator {
public class NewTextualGenericGenerator :  NewTextualShipComponent, Toggleable {
    public float amount;
    public ElementTypes type;
    private bool isOn = true;

    protected override NewShipComponentOutput[] InnerProcess() {
        if (isOn) {
            SetStatus("Generating " + amount.ToString("0.0 ") + type);
            return DistributeAmongChildren(type, amount);
        }
        SetStatus("Off");
        return DistributeAmongChildren(type, 0);
    }

    public void Toggle() {
        isOn = !isOn;
        GameManager.instance.Flow();
    }
}
}