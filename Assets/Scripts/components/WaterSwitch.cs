
using System;

namespace Interstalator {
public class WaterSwitch: GenericSwitch {

    protected override string ComponentName {
        get {
            return "Water Switch";
        }
    }

    protected override ElementTypes[] SwitchType {
        get {
            return new ElementTypes[] {ElementTypes.Water, ElementTypes.Poison};
        }
    }
}
}
