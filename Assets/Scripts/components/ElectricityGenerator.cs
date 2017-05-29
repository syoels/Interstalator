using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator{
public class ElectricityGenerator : ShipComponent {

    protected override bool SetIsOrigin() {
        return true;
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