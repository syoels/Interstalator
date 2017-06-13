using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public abstract class GenericPipe : ShipComponent {
    protected abstract ElementTypes[] PipeType { get; }

    public Directions from = Directions.Right;
    public Directions to = Directions.Left;
    // TODO: add abstract method to change sprite according to "from" & "to" 
    // (astract because graphics adjustments are different for different pipe types)

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