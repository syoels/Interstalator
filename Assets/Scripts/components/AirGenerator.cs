using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public class AirGenerator : ShipComponent {

    public float waterRequirement = 0.3f;
    public float electricityRequirement = 1f;

    protected override string ComponentName {
        get {
            return "Air Generator";
        }
    }

    protected override void SetRequiredInputs() {
        AddRequiredInput(ElementTypes.Water, ElementTypes.Poison);
        AddRequiredInput(ElementTypes.Electricity);
    }


    protected override List<Output> InnerProcess() {
        bool notEnoughWater = incoming[0].amount < waterRequirement;
        bool notEnoughElectrcity = incoming[1].amount < electricityRequirement;

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
        } else if (incoming[0].type == ElementTypes.Poison) {
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
        return new List<Output>();
    }
}
}