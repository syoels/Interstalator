﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator{
public class Garden : Component {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override string getComponentName() {
        return "Garden";
    }

    protected override void InnerUpdateInput(ElementTypes type, float amount) {
        return;
    }

    protected override List<Transmission> InnerProcess() {
        List<Transmission> transmissions = new List<Transmission>();
        return transmissions;
    }
}
}