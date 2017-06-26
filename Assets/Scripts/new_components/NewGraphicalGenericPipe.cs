using UnityEngine; 
using System;
using System.Collections.Generic;

namespace Interstalator {
public class NewGraphicalGenericPipe : NewGraphicalShipComponent {

    public ElementTypes[] possibleTypes;
    protected Dictionary<ElementTypes, int> animatorElements; // Used to map between element enums and animator params.
    protected int amountParamId; 
    protected int elementParamId; 

    protected override ElementTypes[][] DefineInputs() {
        return new ElementTypes[1][] { possibleTypes };
    }

    protected override void SetAnimationParameterIds(){
        animatorElements = new Dictionary<ElementTypes, int>(){
            {ElementTypes.None, 0}, 
            {ElementTypes.Electricity, 1}, 
            {ElementTypes.Water, 2}, 
            {ElementTypes.Poison, 3}, 
        };
        amountParamId = Animator.StringToHash("Amount");
        elementParamId = Animator.StringToHash("Element");
    }

    protected override NewShipComponentOutput[] InnerProcess() {
        
        float amount = inputs[0].amount;
        ElementTypes type = inputs[0].type;
        animator.SetFloat(amountParamId, amount);
        animator.SetInteger(elementParamId, animatorElements[type]);

        return DistributeAmongChildren(type, amount);
    }


}
}

