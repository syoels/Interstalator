using System;
using UnityEngine;

namespace Interstalator {
public class NewGraphicalGarden : NewGraphicalShipComponent {
   
    private int elementParamId;
    private int amountParamId;

    protected override ElementTypes[][] DefineInputs() {
        return new ElementTypes[1][]
        {
            new ElementTypes[] {ElementTypes.Water, ElementTypes.Poison}
        };
    }

    protected override void SetAnimationParameterIds() {
        base.SetAnimationParameterIds();
        elementParamId = Animator.StringToHash("Element");
        amountParamId = Animator.StringToHash("Amount");
    }

    protected override NewShipComponentOutput[] InnerProcess() {

        float amount = inputs[0].amount;
        ElementTypes type = inputs[0].type;
        animator.SetInteger(elementParamId, animatorElements[type]);
        animator.SetFloat(amountParamId, amount);

        return outputs;
    }

}
}

