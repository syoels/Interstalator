using System;
using UnityEngine;

namespace Interstalator {
public class NewGraphicalWasteDisposalMachine : NewGraphicalShipComponent {

    [SerializeField] private bool open;
    private int inParamId; 


    protected override ElementTypes[][] DefineInputs() {
        return new ElementTypes[1][]
        {
            new ElementTypes[] {ElementTypes.WastePerSecond}
        };
    }

    protected override void SetAnimationParameterIds() {
        base.SetAnimationParameterIds();
        inParamId = Animator.StringToHash("In");
    }

    protected override NewShipComponentOutput[] InnerProcess() {
        return outputs;
    }


    public void Open(){
        open = true; 
    }

    public void Close(){
        open = false; 
    }

    public void onWasteReachOpen(){
        animator.SetBool(inParamId, open);
    }

   


}
}

