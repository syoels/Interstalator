using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public class WaterTank : ShipComponent {

    private float _pressure;

    // Use this for initialization
    new void Start () {
        base.Start();
        this._isOrigin = true;
    }

    protected override void SetRequiredInputs() {
        // No require inputs for origins
    }


    protected override string getComponentName() {
        return "Water Tank";
    }

    protected override void InnerUpdateInput(ElementTypes type, float amount) {
        return;
    }

    protected override List<Output> InnerProcess() {
        List<Output> transmissions = new List<Output>();
        foreach (ShipComponent child in children) {
            Output t = new Output(child, ElementTypes.Water, 0f); 
            transmissions.Add(t);
        }

        return transmissions;
    }

}
}

