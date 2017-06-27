using UnityEngine;
using System.Collections;

namespace Interstalator {
public class NewTextualWasteDisposalMachine : NewTextualShipComponent {
    protected override ElementTypes[][] DefineInputs() {
        return new ElementTypes[][]
        {
            new ElementTypes[] { ElementTypes.Electricity },
            new ElementTypes[] { ElementTypes.WastePerSecond }
        };
    }

    protected override NewShipComponentOutput[] InnerProcess() {
        float electricity = inputs[0].amount;
        float wasteRatio = inputs[1].amount;
        // Outgoing waste is based on electricity but avoids goind below zero
        float outgoingWasteRatio = Mathf.Max(
                                       wasteRatio - electricity,
                                       0f
                                   );

        SetStatus(
            string.Format("Failing to dispose of {0:0.00} {1}",
                outgoingWasteRatio,
                ElementTypes.WastePerSecond)
        );

        return DistributeAmongChildren(ElementTypes.WastePerSecond, outgoingWasteRatio);
    }
}
}