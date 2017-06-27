using UnityEngine;
using System.Collections;

namespace Interstalator {
public class NewTextualEngine : NewTextualShipComponent {
    public float minElectricity = 0.1f;
    public float electricityToWasteRatio = 1f;

    protected override ElementTypes[][] DefineInputs() {
        return new ElementTypes[1][] { new ElementTypes[] { ElementTypes.Electricity } };
    }

    protected override NewShipComponentOutput[] InnerProcess() {
        float electricity = inputs[0].amount;
        float wasteRatio = electricity * electricityToWasteRatio;

        if (electricity < minElectricity) {
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
            SetStatus(
                string.Format("Running. Generating {0:0.00} {1}",
                    wasteRatio,
                    ElementTypes.WastePerSecond)
            );
        }

        return DistributeAmongChildren(ElementTypes.WastePerSecond, wasteRatio);
    }
}
}