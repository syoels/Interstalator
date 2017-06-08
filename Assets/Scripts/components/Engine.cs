using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public class Engine : ShipComponent {

    public float minElectricity = 0.1f;

    protected override string ComponentName {
        get {
            return "Engine";
        }
    }

    protected override void SetRequiredInputs() {
        AddRequiredInput(ElementTypes.Electricity);
    }

    protected override List<Output> InnerProcess() {
        List<Output> transmissions = new List<Output>();
        float wasteRatio = incoming[0].amount;
        if (incoming[0].amount < minElectricity) {
            GraphManager.instance.statusController.SetProblem(
                ShipStatusController.ShipSystem.Engine,
                "Engine stopped"
            );
            SetStatus("Not enough electricity!");
            wasteRatio = 0;
        } else {
            GraphManager.instance.statusController.SetOk(
                ShipStatusController.ShipSystem.Engine
            );
            SetStatus("Running. Generating " + wasteRatio.ToString("0.00 ") + ElementTypes.WastePerSecond);
        }

        float wastePerChild = wasteRatio / children.Length;
        foreach (ShipComponent child in children) {
            transmissions.Add(new Output(child, ElementTypes.WastePerSecond, wastePerChild));
        }
        return transmissions;
    }


}
}