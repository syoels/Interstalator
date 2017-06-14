using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public abstract class GenericGenerator : AltShipComponent {
    public float amount;

    protected abstract ElementTypes GeneratorType { get; }

    /// <summary>
    /// Sends to each child an equal part of the total amount
    /// </summary>
    /// <returns>List of outputs to transfer to the children</returns>
    protected override List<Output> InnerProcess() {
        float amountPerChild = amount / children.Length;
        List<Output> outputs = new List<Output>();
        foreach (ShipComponent child in children) {
            Output t = new Output(child, GeneratorType, amountPerChild); 
            outputs.Add(t);
        }

        SetStatus("Sending " + amount + " " + GeneratorType);
        return outputs;
    }
}
}