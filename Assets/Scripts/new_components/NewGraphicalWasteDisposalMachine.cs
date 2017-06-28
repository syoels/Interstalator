using System;
using UnityEngine;

namespace Interstalator {
public class NewGraphicalWasteDisposalMachine : NewGraphicalShipComponent {

    [SerializeField] private bool open;
    private int inParamId; 
    private int speedParamId; 
    private int wasteRatioParamId; 
    private NewGraphicalWastePile wastePile; 

    protected override ElementTypes[][] DefineInputs() {
        return new ElementTypes[2][]
        {
            new ElementTypes[] {ElementTypes.WastePerSecond}, 
            new ElementTypes[] {ElementTypes.Electricity}
        };
    }

    void Start(){
        wastePile = (NewGraphicalWastePile)children[0];
    }

    protected override void SetAnimationParameterIds() {
        base.SetAnimationParameterIds();
        inParamId = Animator.StringToHash("In");
        speedParamId = Animator.StringToHash("Speed");
        wasteRatioParamId = Animator.StringToHash("WasteRatio");
    }

    protected override NewShipComponentOutput[] InnerProcess() {
        float ratio = inputs[0].amount; 
        float speed = inputs[1].amount; 
        animator.SetFloat(wasteRatioParamId, ratio);
        animator.SetFloat(speedParamId, speed);
        NewShipComponentOutput[] empty = new NewShipComponentOutput[0]{ };
        return empty;
    }
        
    //SHould be run through event when "teeth" are open
    public void Open(){
        open = true; 
    }

    //SHould be run through event when "teeth" are closed
    public void Close(){
        open = false; 
    }

    public void onWasteReachOpen(){
        animator.SetBool(inParamId, open);
    }

    public void onWasteReachPile(){
        wastePile.pileSize++;
    }

   


}
}

