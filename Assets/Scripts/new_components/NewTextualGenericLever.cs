using UnityEngine;
using System.Collections;

namespace Interstalator {
/// <summary>
/// Chooses between one of its given inputs
/// </summary>
public class NewTextualGenericLever : NewTextualShipComponent {
    public ElementTypes[] possibleTypes;
    [Tooltip("This number must correspound with the number of incoming connections")]
    public int numberOfInputs;
    [SerializeField] private int currentInputIndex;

    protected override ElementTypes[][] DefineInputs() {
        ElementTypes[][] inputDefinitions = new ElementTypes[numberOfInputs][];
        for (int i = 0; i < inputDefinitions.Length; i++) {
            inputDefinitions[i] = possibleTypes;
        }
        return inputDefinitions;
    }

    protected override NewShipComponentOutput[] InnerProcess() {
        float amount = inputs[currentInputIndex].amount;
        ElementTypes type = inputs[currentInputIndex].type;

        SetStatus(
            string.Format("Sending {0:0.00} {1} to {2} children",
                amount,
                type,
                children.Length)
        );

        return DistributeAmongChildren(type, amount);
    }

    public void Toggle() {
        currentInputIndex = (currentInputIndex+ 1) % children.Length;
        GameManager.instance.Flow();
    }
}
}