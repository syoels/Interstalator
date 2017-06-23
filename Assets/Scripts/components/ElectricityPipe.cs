using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public class ElectricityPipe : GenericPipe {
    [Range (1f, 20f)]
    public float ElectricityToSpeedRAtio = 10f;
    [SerializeField]
    float electricity = 0f; 
    float prevElectricity = 0f;
    protected override string ComponentName {
        get {
            return "Electricity Pipe";
        }
    }

    protected override ElementTypes[] PipeType {
        get {
            return new ElementTypes[] { ElementTypes.Electricity };
        }
    }

    protected override void SetAnimationParams(){
        base.SetAnimationParams();
        prevElectricity = electricity; 
        electricity = this.incoming[0].amount;
        SetAnimationFloatParam("electricity", electricity);
        SetAnimationFloatParam("speed", electricity * ElectricityToSpeedRAtio);
        if (prevElectricity == 0f && electricity > 0f) {
            SetAnimationTriggerParam("flow");
        }
    }
}
}