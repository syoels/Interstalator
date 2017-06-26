using UnityEngine;
using System.Collections;

namespace Interstalator {
public class NewTextualGarden : NewTextualShipComponent {
    protected override ElementTypes[][] DefineInputs() {
        return new ElementTypes[][] { new ElementTypes[] { ElementTypes.Water, ElementTypes.Poison } };
    }

    protected override NewShipComponentOutput[] InnerProcess() {
        if (inputs[0].amount > 0) {
            SetStatus("Being watered");
        } else {
            SetStatus("Not being watered");
        }

        return new NewShipComponentOutput[] {};
    }
}
}