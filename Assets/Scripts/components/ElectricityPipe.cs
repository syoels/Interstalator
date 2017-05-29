﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public class ElectricityPipe : GenericPipe {

    protected override string ComponentName {
        get {
            return "Electricity Pipe";
        }
    }

    protected override ElementTypes PipeType {
        get {
            return ElementTypes.Electricity;
        }
    }
}
}