using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public class ConveyorBelt : GenericPipe {

    protected override string ComponentName {
        get {
            return "Conveyor Belt";
        }
    }

    protected override ElementTypes[] PipeType {
        get {
            return new ElementTypes[] {ElementTypes.NuclearWasteRatio};
        }
    }
}
}
