using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator{
public class WaterSwitch : ShipComponent {


    protected override string getComponentName() {
        return "Water Switch";
    }

    protected override void InnerUpdateInput(ElementTypes type, float amount) {
        return;
    }

    protected override List<Transmission> InnerProcess() {
        List<Transmission> transmissions = new List<Transmission>();
        return transmissions;
    }

    public void Redistribute(float[] newDistribution) {
        return;
    }
}
}