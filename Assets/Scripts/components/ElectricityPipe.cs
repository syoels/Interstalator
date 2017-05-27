using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator{
public class ElectricityPipe : ShipComponent {

    protected override void SetRequiredInputs() {
        AddRequiredInput(ElementTypes.Electricity);
    }

    protected override string getComponentName() {
        return "Electricity Pipe";
    }

    protected override void InnerUpdateInput(ElementTypes type, float amount) {
        return;
    }

    protected override List<Transmission> InnerProcess() {
        List<Transmission> transmissions = new List<Transmission>();
        foreach (ShipComponent child in children) {
            Transmission t = new Transmission(child, ElementTypes.Electricity, 0f); 
            transmissions.Add(t);
        }

        return transmissions;
    }
}
}