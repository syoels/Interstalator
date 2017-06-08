using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public class WasteDisposalMachine : ShipComponent {
    protected override string ComponentName {
        get {
            return "Waste Disposal";
        }
    }

    protected override void SetRequiredInputs() {
        AddRequiredInput(ElementTypes.Electricity);
        AddRequiredInput(ElementTypes.WastePerSecond);
    }

    protected override List<Output> InnerProcess() {
        List<Output> outputs = new List<Output>();

        float incomingElectricity = incoming[0].amount;
        float incomingWasteRatio = incoming[1].amount;
        // Outgoing waste is based on electricity but avoids goind below zero
        float outgoingWasteRatio = Mathf.Max(
                                  incomingWasteRatio - incomingElectricity,
                                  0f
                              );

        float wastePerChild = outgoingWasteRatio / children.Length;

        foreach (ShipComponent child in children) {
            outputs.Add(new Output(
                child,
                ElementTypes.WastePerSecond,
                wastePerChild
            ));
        }

        SetStatus("Failing to dispose of " + outgoingWasteRatio + " waste");
        return outputs;
    }


}
}