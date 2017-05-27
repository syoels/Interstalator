using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator{
public class WaterPipe : ShipComponent {

    protected override string getComponentName() {
        return "Water Pipe";
    }

    protected override void SetRequiredInputs() {
        AddRequiredInput(ElementTypes.Water);
    }

    protected override void InnerUpdateInput(ElementTypes type, float amount) {
        return;
    }

    protected override List<Transmission> InnerProcess() {
        List<Transmission> transmissions = new List<Transmission>();
        foreach (ShipComponent child in children) {
            Transmission t = new Transmission(child, ElementTypes.Water, 0f); 
            transmissions.Add(t);
        }

        return transmissions;
    }
}
}
