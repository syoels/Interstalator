
using System;

namespace Interstalator {
public class ElectricSwitch: GenericSwitch {

    protected override string ComponentName {
        get {
            return "Electric Switch";
        }
    }

    protected override ElementTypes[] SwitchType {
        get {
            return new ElementTypes[] {ElementTypes.Electricity};
        }
    }
}
}

