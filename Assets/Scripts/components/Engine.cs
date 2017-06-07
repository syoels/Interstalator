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
        if (incoming[0].amount < minElectricity) {
            GraphManager.instance.statusController.SetProblem(
                ShipStatusController.ShipSystem.Engine,
                "Engine stopped"
            );
        } else {
            GraphManager.instance.statusController.SetOk(
                ShipStatusController.ShipSystem.Engine
            );
        }
        ShipComponent[] conveyerBelts = GetChildrenOfType<ConveyorBelt>();
        float ratioPerBelt = incoming[0].amount / conveyerBelts.Length;
        foreach (ConveyorBelt belt in conveyerBelts) {
            transmissions.Add(new Output(belt, ElementTypes.NuclearWasteRatio, ratioPerBelt));
        }
        return transmissions;
    }


}
}