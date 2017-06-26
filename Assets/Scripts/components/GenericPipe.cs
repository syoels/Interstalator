using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public abstract class GenericPipe : AltShipComponent {

    //Animation related
    protected int currElement = 0; // 0 = None, 1 = Main, 2 = Alternative, etc. 
    protected float currAmount = 0f; 
    protected int currElementParamId;
    protected int currAmountParamId;

    protected abstract ElementTypes[] PipeType { get; }


    /// <summary>
    /// Pipes only transfer one element type to their children 
    /// </summary>
    protected override void SetRequiredInputs() {
        AddRequiredInput(PipeType);
    }

    protected override List<Output> InnerProcess() {
        currAmount = incoming[0].amount;
        float amountPerChild = currAmount / children.Length;

        List<Output> outputs = new List<Output>();
        foreach (ShipComponent child in children) {
            Output t = new Output(child, (ElementTypes)incoming[0].type, amountPerChild); 
            outputs.Add(t);
        }

        SetStatus("Transferring " + amountPerChild.ToString("0.00 ") + incoming[0].type);
        return outputs;
    }
}
}