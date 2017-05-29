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
        float amount = (float)incoming[0].amount / children.Length;

        List<Output> outputs = new List<Output>();
        foreach (ShipComponent child in children) {
            Output t = new Output(child, ElementTypes.Water, amount);
            outputs.Add(t);
        }

        SetStatus("Distributing " + incoming[0].amount + " Water equally");
        return outputs;
    }

    public void ApplyDistribution(float[] newDistribution) {
        return;
    }
}
}