using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public class Garden : ShipComponent {

    protected override string ComponentName {
        get {
            return "Garden";
        }
    }

    protected override void SetRequiredInputs() {
        AddRequiredInput(ElementTypes.Water);
    }

    protected override List<Output> InnerProcess() {
        if (incoming[0].amount > 0) {
            SetStatus("Being watered");
        } else {
            SetStatus("Not being watered");
        }
        List<Output> transmissions = new List<Output>();
        return transmissions;
    }
}
}