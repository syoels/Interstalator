using UnityEngine;
using System.Collections;

namespace Interstalator {
// Receives water and sends it to one of its children
public class NewTextualWaterHoze : NewTextualShipComponent {
    [SerializeField] private int connectedChildIndex;

    protected override ElementTypes[][] DefineInputs() {
        return new ElementTypes[1][]
        {
            new ElementTypes[] {ElementTypes.Water, ElementTypes.Poison}
        };
    }

    protected override NewShipComponentOutput[] InnerProcess() {
        float amount = inputs[0].amount;
        ElementTypes type = inputs[0].type;

        for (int i = 0; i < children.Length; i++) {
            if (i == connectedChildIndex) {
                outputs[i].Set(type, amount);                
            } else {
                outputs[i].Set(type, 0);
            }
        }

        SetStatus(
            string.Format("Watering {0} with {1:0.00} {2}",
                outputs[connectedChildIndex].component.name,
                amount,
                type)
        );

        return outputs;
    }

    public void Toggle() {
        connectedChildIndex = (connectedChildIndex + 1) % children.Length;
        GameManager.instance.Flow();
    }
}
}