using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public class WaterTank : GenericGenerator {
    protected override ElementTypes GeneratorType {
        get {
            return ElementTypes.Water;
        }
    }

    protected override string ComponentName {
        get {
            return "Water Tank";
        }
    }
}
}

