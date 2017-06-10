using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public class WaterHoze : ShipComponent {

    public int connectedChildIndex;

    protected override void SetRequiredInputs() {
        AddRequiredInput(ElementTypes.Water, ElementTypes.Poison);
    }

    protected override string ComponentName {
        get {
            return "Water Hoze";
        }
    }

    protected override List<Output> InnerProcess() {
        List<Output> outputs = new List<Output>();

        float liquidAmount = incoming[0].amount;
        ElementTypes liquidType = (ElementTypes)incoming[0].type;
        for (int i = 0; i < children.Length; i++) {
            if (i == connectedChildIndex) {
                outputs.Add(new Output(children[i], liquidType, liquidAmount));
            } else {
                outputs.Add(new Output(children[i], liquidType, 0f));
            }
        }

        SetStatus("Watering " + children[connectedChildIndex] + " with " + liquidAmount.ToString("0.00 ") + liquidType);
        return outputs;
    }

    // Replace child with next possible connect (only works on one child at this point
    public override void Interact() {
        connectedChildIndex = (connectedChildIndex + 1) % children.Length;
        GraphManager.instance.Flow();
    }

    public override bool IsInteractable() {
        return true;
    }

    public override string InteractionDescription {
        get {
            string message = "Connect hoze to: ";
            message += children[(connectedChildIndex + 1) % children.Length];
            return message;
        }
    }
}
}