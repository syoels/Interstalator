using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public class WaterTank : Component {

    private float _pressure;

    public float pressure {
        set { 
            _pressure = value; 
        }
        get { 
            return _pressure;
        }
    }

    protected override string getComponentName() {
        return "Water Tank";
    }

    protected override void InnerUpdateInput(ElementTypes type, float amount) {
        return;
    }

    protected override List<Transmission> InnerProcess() {
        List<Transmission> transmissions = new List<Transmission>();
        foreach (Component child in children) {
            Transmission t = new Transmission(); 
            t.child = child; 
            t.amount = 0f; 
            t.type = ElementTypes.Water;
            transmissions.Add(t);
        }

        return transmissions;
    }

}
}

