using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator{
public class ElectricityGenerator : ShipComponent {

    // Use this for initialization
    new void Start () {
        base.Start();
        this._isOrigin = true;
    }

    protected override void SetRequiredInputs() {
        // No required inputs for origins
    }

    protected override string getComponentName() {
        return "Electricity Generator";
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