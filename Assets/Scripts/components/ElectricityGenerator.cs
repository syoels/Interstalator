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
        
    protected override List<Output> InnerProcess() {
        List<Output> transmissions = new List<Output>();
        foreach (ShipComponent child in children) {
            Output t = new Output(child, ElementTypes.Electricity, 0f); 
            transmissions.Add(t);
        }

        return transmissions;
    }
}
}