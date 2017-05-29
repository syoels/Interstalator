using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator{
public class ElectricityGenerator : GenericGenerator {
    protected override ElementTypes GeneratorType {
        get {
            return ElementTypes.Electricity;
        }
    }

    protected override string ComponentName {
        get {
            return "Electricity Generator";
        }
    }
}
}