using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator{
public class AirGenerator : ShipComponent {

    protected override void SetRequiredInputs() {
        AddRequiredInput(ElementTypes.Water);
        AddRequiredInput(ElementTypes.Electricity);
    }
        

    protected override List<Output> InnerProcess() {
        List<Output> transmissions = new List<Output>();
        return transmissions;
    }
}
}