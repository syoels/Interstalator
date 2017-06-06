using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public class AirGenerator : ShipComponent {

    public float waterRequirement = 0.3f;

    protected override string ComponentName {
        get {
            return "Air Generator";
        }
    }

    protected override void SetRequiredInputs() {
        AddRequiredInput(ElementTypes.Water);
        AddRequiredInput(ElementTypes.Electricity);
    }


    protected override List<Output> InnerProcess() {
        if (incoming[0].amount > waterRequirement) {
            SetStatus("Creating air");
            GraphManager.instance.statusController.SetOk(
                ShipStatusController.ShipSystem.Air
            );
        } else {
            SetStatus("Not enough water");
            GraphManager.instance.statusController.SetProblem(
                ShipStatusController.ShipSystem.Air,
                "No air"
            );
        }


        // Not outputtin anything
        return new List<Output>();
    }
}
}