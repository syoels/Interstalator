﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public abstract class GenericPipe : ShipComponent {
    protected abstract ElementTypes PipeType { get; }

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
            Output t = new Output(child, PipeType, amount); 
            outputs.Add(t);
        }

        SetStatus("Transferring " + amount + " " + PipeType);
        return outputs;
    }
}
}