using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public class WaterSwitch : ShipComponent {
    protected override string ComponentName {
        get {
            return "Water Switch";
        }
    }

    protected override void SetRequiredInputs() {
        AddRequiredInput(ElementTypes.Water);
    }


    protected override List<Output> InnerProcess() {
        List<Output> transmissions = new List<Output>();
        foreach (ShipComponent child in children) {
            Output t = new Output(child, ElementTypes.Water, 0f);
            transmissions.Add(t);
        }

        SetStatus("Dividing water to X");
        return transmissions;
    }

    public void ApplyDistribution(float[] newDistribution) {
        return;
    }
}
}