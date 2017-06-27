using System;
using UnityEngine;

namespace Interstalator {
public class NewGraphicalWaterHoze : NewGraphicalShipComponent, Toggleable{
    
    [SerializeField] private int direction = 1;
    private int directions = 3;
    private int directionParamId;
    private int elementParamId;
    private int amountParamId;
    private int startParamId;
    float amount = 0f; 
    float prevAmount = 0f; 

    protected override ElementTypes[][] DefineInputs() {
        return new ElementTypes[1][]
        {
            new ElementTypes[] {ElementTypes.Water, ElementTypes.Poison}
        };
    }

    protected override void SetAnimationParameterIds() {
        base.SetAnimationParameterIds();
        directionParamId = Animator.StringToHash("Direction");
        elementParamId = Animator.StringToHash("Element");
        amountParamId = Animator.StringToHash("Amount");
        startParamId = Animator.StringToHash("Start");
        amountParamId = Animator.StringToHash("Amount");
    }

    protected override NewShipComponentOutput[] InnerProcess() {

        prevAmount = amount;
        amount = inputs[0].amount;
        ElementTypes type = inputs[0].type;

        for (int i = 0; i < children.Length; i++) { 
            if (i == direction) {
                outputs[i].Set(type, amount);                
            } else {
                outputs[i].Set(type, 0);
            }
        }

        animator.SetInteger(directionParamId, direction);
        animator.SetInteger(elementParamId, animatorElements[type]);
        animator.SetFloat(amountParamId, amount);
        if (prevAmount == 0f && amount > 0f) {
            animator.SetTrigger(startParamId);
        }

        return outputs;
    }


    public void Toggle(){
        direction = (direction + 1) % directions;
        animator.SetInteger(directionParamId, direction);
        GameManager.instance.flowManager.Flow();
    }
}
}

