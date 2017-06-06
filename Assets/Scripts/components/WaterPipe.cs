using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public class WaterPipe : GenericPipe {

    protected override string ComponentName {
        get {
            return "Water Pipe";
        }
    }

    protected override ElementTypes[] PipeType {
        get {
            return new ElementTypes[] {ElementTypes.Water, ElementTypes.Poison};
        }
    }
}
}
