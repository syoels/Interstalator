using UnityEngine;
using System;
using System.Linq;

namespace Interstalator {
public class NewGraphicalAirMachine : NewGraphicalShipComponent{

    private int elementParamInt; 
    public float minElectricity = 0.2f; 
    public float minLiquid = 0.2f;    

    protected override ElementTypes[][] DefineInputs() {
        ElementTypes[] electricityElements = { ElementTypes.Electricity };
        ElementTypes[] liquidElements = { ElementTypes.Water, ElementTypes.Poison };
        return new ElementTypes[2][]{electricityElements, liquidElements};
        }

    protected override NewShipComponentOutput[] InnerProcess() {
        float electricity = inputs[0].amount;
        ElementTypes type = inputs[1].type;
        float amount = inputs[1].amount;
        ElementTypes generatedElement = ElementTypes.Air;

        if (electricity < minElectricity || amount < minLiquid) {
            generatedElement = ElementTypes.None;
            GameManager.instance.shipStatus.SetProblem(
                ShipSystem.Air, 
                "No air!");      
        } else if (type == ElementTypes.Poison) {
            generatedElement = ElementTypes.PoisonousAir;
            GameManager.instance.shipStatus.SetProblem(
                ShipSystem.Air, 
                "Poisonous air!");  
        } 

        animator.SetInteger(elementParamInt, animatorElements[generatedElement]);
        return DistributeAmongChildren(generatedElement, amount * electricity);
    }

    protected override void SetAnimationParameterIds() {
        elementParamInt = Animator.StringToHash("Element"); 
        }
}
}

