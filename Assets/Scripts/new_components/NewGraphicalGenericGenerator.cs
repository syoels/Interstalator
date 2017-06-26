using System;
using UnityEngine;

namespace Interstalator {
public class NewGraphicalGenericGenerator : NewGraphicalShipComponent{

    public float amount;
    public ElementTypes type;
    protected int amountParamId; 
    protected int elementParamId; 
    protected bool isWorking = true;

    protected override void SetAnimationParameterIds(){
        amountParamId = Animator.StringToHash("Amount");
        elementParamId = Animator.StringToHash("Element");
    }

    protected override NewShipComponentOutput[] InnerProcess() {
        float generatedAmount = isWorking ? amount : 0f;
        animator.SetFloat(amountParamId, generatedAmount);
        animator.SetInteger(elementParamId, animatorElements[type]);
        return DistributeAmongChildren(type, amount);
    }

    public void Toggle(){
        isWorking = !isWorking;
        GameManager.instance.Flow();
    }

    public void SetAmount(float newAmount){
        amount = newAmount; 
        GameManager.instance.Flow();
    }


}
}

