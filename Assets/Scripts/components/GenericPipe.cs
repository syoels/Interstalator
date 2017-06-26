using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public abstract class GenericPipe : AltShipComponent {
    protected abstract ElementTypes[] PipeType { get; }

    protected int currElement; //0 = Empty, 1 = Main, 2 = Alternative, etc.
    protected float threshold = 0.001f; // Above threshold: flow, below: stop.
    protected float amountToSpeedRatio = 1f; 
    protected float amount = 0f; 

    /// <summary>
    /// Pipes only transfer one element type to their children 
    /// </summary>
    protected override void SetRequiredInputs() {
        AddRequiredInput(PipeType);
    }

    protected override List<Output> InnerProcess() {
        float amount = incoming[0].amount / children.Length;

        List<Output> outputs = new List<Output>();
        foreach (ShipComponent child in children) {
            Output t = new Output(child, (ElementTypes)incoming[0].type, amount); 
            outputs.Add(t);
        }

        SetStatus("Transferring " + amount.ToString("0.00 ") + incoming[0].type);
        return outputs;
    }
}
}