using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public class AirGenerator : ShipComponent {

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
        List<Output> transmissions = new List<Output>();
        SetStatus("Creating air");
        GraphManager.instance.statusController.SetOk(
            ShipStatusController.ShipSystem.Air
        );
        return transmissions;
    }
}
}