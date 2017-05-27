using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator{
public class Garden : ShipComponent {

    protected override string getComponentName() {
        return "Garden";
    }

    protected override void SetRequiredInputs() {
        AddRequiredInput(ElementTypes.Water);
    }

    protected override List<Output> InnerProcess() {
        List<Output> transmissions = new List<Output>();
        return transmissions;
    }
}
}