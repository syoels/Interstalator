using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public class WaterPipe : GenericPipe {

    [SerializeField] ElementTypes? currElement = ElementTypes.None;
    private ElementTypes? prevElement = ElementTypes.None;
    [SerializeField] float amount; 
    float prevAmount = 0f;
    [Range (1f, 20f)]
    public float WaterToSpeedRAtio = 10f;

    protected override string ComponentName {
        get {
            return "Water Pipe";
        }
    }

    protected override ElementTypes[] PipeType {
        get {
            return new ElementTypes[] {ElementTypes.Water, ElementTypes.Poison};
        }
    }

    protected override void SetAnimationParams(){
        base.SetAnimationParams();
        prevElement = currElement; 
        currElement = this.incoming[0].type;
        prevAmount = amount;
        amount = this.incoming[0].amount;
        switch (prevElement) {
        case ElementTypes.Water:
            SetAnimationBoolParam("fromWater", true);
            SetAnimationBoolParam("fromPoison", false);
            break;
        case ElementTypes.Poison:
            SetAnimationBoolParam("fromPoison", true);
            SetAnimationBoolParam("fromWater", false);
            break;
        default:
            SetAnimationBoolParam("fromPoison", false);
            SetAnimationBoolParam("fromWater", false);
            break;
        }
        switch (currElement) {
        case ElementTypes.Water:
            SetAnimationBoolParam("toWater", true);
            SetAnimationBoolParam("toPoison", false);
            break;
        case ElementTypes.Poison:
            SetAnimationBoolParam("toPoison", true);
            SetAnimationBoolParam("toWater", false);
            break;
        default:
            SetAnimationBoolParam("toPoison", false);
            SetAnimationBoolParam("toWater", false);
            break;
        }

        SetAnimationFloatParam("speed", amount * WaterToSpeedRAtio);
        if (prevAmount == 0f && amount > 0f) {
            SetAnimationTriggerParam("flow");
        }
    }
}
}
