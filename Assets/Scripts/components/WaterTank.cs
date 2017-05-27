﻿using System;
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

    // Use this for initialization
    void Start () {
        this._isOrigin = true;
    }

    // Update is called once per frame
    void Update () {

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
            Transmission t = new Transmission(child, ElementTypes.Water, 0f); 
            transmissions.Add(t);
        }

        return transmissions;
    }

}
}

