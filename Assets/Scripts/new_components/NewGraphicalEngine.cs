using System;
using UnityEngine;

namespace Interstalator {
public class NewGraphicalEngine : NewGraphicalShipComponent{

    [SerializeField] private float minElectricity = 0.2f; 


    protected override ElementTypes[][] DefineInputs() {
        return new ElementTypes[1][]{ new ElementTypes[] { ElementTypes.Electricity } };
    }


    protected override NewShipComponentOutput[] InnerProcess() {
        float electricity = inputs[0].amount;
        float wasteRatio = electricity;

        if (electricity < minElectricity) {
            GameManager.instance.shipStatus.SetProblem(
                ShipSystem.Engine,
                "Engine stopped"
            );
            wasteRatio = 0f;
        } else {
            GameManager.instance.shipStatus.SetOk(
                ShipSystem.Engine
            );
        }

        // Window //TODO: This is actuall a symptom. maybe it should be handled through ShipStatusController.cs
        SpaceshipWindow[] windows = FindObjectsOfType<SpaceshipWindow>();
        foreach (SpaceshipWindow window in windows) {
            window.Speed = electricity;
        }

        return DistributeAmongChildren(ElementTypes.WastePerSecond, wasteRatio);
    }


}
}

