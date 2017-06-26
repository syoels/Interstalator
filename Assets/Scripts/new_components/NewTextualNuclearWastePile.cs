using UnityEngine;
using System.Collections;

namespace Interstalator {
public class NewTextualNuclearWastePile : NewTextualShipComponent {
    public int pileSize;
    public GameObject wastePrefab;
    private const int MAX_PILE_SIZE = 8;
    private float lastAddTime = 0f;
    private float wasteRatio = 0f;

    // Used to place the nuclear waste on the screen
//    private Vector2 wastePosition = new Vector2(23.5f, -9.6f);
//    private const float MAX_WASTE_Y = 2.4f;

    void Update() {
        if (wasteRatio > 0 && pileSize < MAX_PILE_SIZE) {
            // How many seconds to wait before increasing pile size
            float pileIncreaseRate = 1f / wasteRatio;
            if (Time.time - lastAddTime > pileIncreaseRate) {
                pileSize = Mathf.Min(pileSize + 1, MAX_PILE_SIZE);
                lastAddTime = Time.time;
                GameManager.instance.Flow();
            }
        }
    }

    protected override ElementTypes[][] DefineInputs() {
        return new ElementTypes[][]
        {
            new ElementTypes[] { ElementTypes.Water, ElementTypes.Poison },
            new ElementTypes[] { ElementTypes.WastePerSecond },
        };
    }

    protected override NewShipComponentOutput[] InnerProcess() {
        ElementTypes outputType = inputs[0].type;
        wasteRatio = inputs[1].amount;

        if (pileSize > 0) {
            outputType = ElementTypes.Poison;
        }

        SetStatus("Pile size: " + pileSize);
        return DistributeAmongChildren(outputType, inputs[0].amount);

    }
}
}