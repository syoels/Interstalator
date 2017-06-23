using UnityEngine;
using System.Collections;

namespace Interstalator {
public class NewTextualAirMachine : NewTextualShipComponent {
    public float waterRequirement = 0.3f;
    public float electricityRequirement = 1f;

    protected override ElementTypes[][] DefineInputs() {
        return new ElementTypes[][]
        {
            new ElementTypes[] { ElementTypes.Water, ElementTypes.Poison },
            new ElementTypes[] { ElementTypes.Electricity }
        };
    }

    protected override NewShipComponentOutput[] InnerProcess() {
        bool notEnoughWater = inputs[0].amount < waterRequirement;
        bool notEnoughElectrcity = inputs[1].amount < electricityRequirement;

        if (notEnoughWater || notEnoughElectrcity) {
            GameManager.instance.shipStatus.SetProblem(
                ShipSystem.Air,
                "No air!"
            );
            if (notEnoughElectrcity) {
                SetStatus("Not enough electricity");
            } else {
                SetStatus("Not enough water");
            }
        } else if (inputs[0].type == ElementTypes.Poison) {
            GameManager.instance.shipStatus.SetProblem(
                ShipSystem.Air,
                "Posioned!"
            );
            SetStatus("Generating poisoned air");
        } else {
            GameManager.instance.shipStatus.SetOk(
                ShipSystem.Air
            );
            SetStatus("Generating air");
        }

        // Not outputting anything
        return new NewShipComponentOutput[] {};
    }
}
}