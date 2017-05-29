using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public class WaterTank : ShipComponent {

    private float _pressure;

    override protected bool SetIsOrigin() {
        return true;
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

