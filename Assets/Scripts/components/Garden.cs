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

    protected override void InnerUpdateInput(ElementTypes type, float amount) {
        return;
    }

    protected override List<Transmission> InnerProcess() {
        List<Transmission> transmissions = new List<Transmission>();
        return transmissions;
    }
}
}