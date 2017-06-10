using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public class WaterLever : ShipComponent {

    public int activeInput = 0;

    protected override string ComponentName {
        get {
            return "Water lever";
        }
    }

    protected override void SetRequiredInputs() {
        AddRequiredInput(ElementTypes.Water, ElementTypes.Poison);
        AddRequiredInput(ElementTypes.Water, ElementTypes.Poison);
    }

    protected override List<Output> InnerProcess() {
        List<Output> outputs = new List<Output>();

        float liquidAmount = incoming[activeInput].amount;
        ElementTypes liquidType = (ElementTypes)incoming[activeInput].type;

        float liquidDistribution = liquidAmount / children.Length;
        foreach (ShipComponent child in children) {
            outputs.Add(new Output(child, liquidType, liquidDistribution));
        }

        SetStatus("Transferring " + liquidAmount.ToString("0.00 ") + liquidType);

        return outputs;
    }

    public override void Interact() {
        activeInput = (activeInput + 1) % incoming.Count;
        GraphManager.instance.Flow();
    }

    public override bool IsInteractable() {
        // Maybe should require a wrench?
        return true;
    }

    public override string InteractionDescription {
        get {
            return "Change water lever input";
        }
    }
}
}