using UnityEngine; 
using System;
using System.Collections.Generic;

namespace Interstalator {
public class NewGraphicalGenericPipe : NewGraphicalShipComponent {

    public ElementTypes[] possibleTypes;
    protected int amountParamId; 
    protected int elementParamId; 

    protected override ElementTypes[][] DefineInputs() {
        return new ElementTypes[1][] { possibleTypes };
    }

    protected override void SetAnimationParameterIds(){  
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

