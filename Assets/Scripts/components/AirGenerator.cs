using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator{
public class AirGenerator : ShipComponent {

    protected override void SetRequiredInputs() {
        AddRequiredInput(ElementTypes.Water);
        AddRequiredInput(ElementTypes.Electricity);
    }

    protected override string getComponentName() {
        return "Air Generator";
    }

    protected override void InnerUpdateInput(ElementTypes type, float amount) {
        return;
    }

    protected override List<Transmission> InnerProcess() {
        List<Transmission> transmissions = new List<Transmission>();
        return transmissions;
    }
}
}