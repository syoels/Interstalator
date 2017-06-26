using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public class Engine : AltShipComponent {

    public float minElectricity = 0.1f;
    public float electricityToWasteRatio = 1f;
    private float electricity;

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
        electricity = incoming[0].amount;
        float wasteRatio = electricity * electricityToWasteRatio;
        if (incoming[0].amount < minElectricity) {
            GameManager.instance.shipStatus.SetProblem(
                ShipSystem.Engine,
                "Engine stopped"
            );
            SetStatus("Not enough electricity!");
            wasteRatio = 0;
        } else {
            GameManager.instance.shipStatus.SetOk(
                ShipSystem.Engine
            );
            SetStatus("Running. Generating " + wasteRatio.ToString("0.00 ") + ElementTypes.WastePerSecond);
        }

        // Window //TODO: This is actuall a symptom. maybe it should be handled through ShipStatusController.cs
        SpaceshipWindow[] windows = FindObjectsOfType<SpaceshipWindow>();
        foreach (SpaceshipWindow window in windows) {
            window.Speed = electricity;
        }

        // Waste
        float wastePerChild = wasteRatio / children.Length;
        foreach (ShipComponent child in children) {
            transmissions.Add(new Output(child, ElementTypes.WastePerSecond, wastePerChild));
        }
        return transmissions;
    }


}
}