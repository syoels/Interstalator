using System;
using UnityEngine;

namespace Interstalator {
public class NewGraphicalGenericLever : NewGraphicalShipComponent, Toggleable {

    public ElementTypes[] possibleTypes;
    private int CurrInputParamId;
    [SerializeField] private int currInput = 0; 


    protected override ElementTypes[][] DefineInputs() {
        return new ElementTypes[2][] { possibleTypes, possibleTypes };
    }

    protected override void SetAnimationParameterIds() {
        base.SetAnimationParameterIds();
        CurrInputParamId = Animator.StringToHash("Input");
    }

    protected override NewShipComponentOutput[] InnerProcess() {

        float amount = inputs[currInput].amount;
        ElementTypes type = inputs[currInput].type;
        return DistributeAmongChildren(type, amount);
    }

    public void Toggle() {
        currInput = (currInput + 1) % inputs.Length;
        animator.SetInteger(CurrInputParamId, currInput);
        GameManager.instance.Flow();
    }
}
}
