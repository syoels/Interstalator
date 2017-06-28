using UnityEngine;
using System.Collections;

namespace Interstalator {
public class NewGraphicalWastePile : NewGraphicalShipComponent {
    private const int MAX_PILE_SIZE = 8;
    private int sizeParamId;
    private int pileThreshold = 0;

    [SerializeField] private int _pileSize = 0;
    private int prevPileSize = 0;
    public int pileSize {
        get {
            return _pileSize;
        }
        set {
            prevPileSize = _pileSize;
            if (value == _pileSize) {
                return;
            }

            if (value < _pileSize) {
                _pileSize = Mathf.Max(0, value);
            } else {
                _pileSize = Mathf.Min(MAX_PILE_SIZE, value);
            }

            animator.SetInteger(sizeParamId, _pileSize);
            if ((prevPileSize <= pileThreshold && _pileSize > pileThreshold) || 
                (_pileSize <= pileThreshold && prevPileSize > pileThreshold)) {
                Debug.Log("Flowing from pile");
                GameManager.instance.Flow();
            }

        }
    }

    protected override void SetAnimationParameterIds() {
        sizeParamId = Animator.StringToHash("Size");
        animator.SetInteger(sizeParamId, _pileSize);
    }

    protected override ElementTypes[][] DefineInputs() {
        return new ElementTypes[1][]
        {
            new ElementTypes[] { ElementTypes.Water, ElementTypes.Poison }
            // Maybe not necessary...
//            new ElementTypes[] { ElementTypes.WastePerSecond }
        };
    }

    protected override NewShipComponentOutput[] InnerProcess() {
        ElementTypes outputType = inputs[0].type;

        if (pileSize > 0) {
            outputType = ElementTypes.Poison;
        }

        // Sends poison if we have nuclear waste
        return DistributeAmongChildren(outputType, inputs[0].amount);
    }

    // Should be used by animation trigger
    public void addNuclearWaste() {
        pileSize = Mathf.Min(pileSize + 1, MAX_PILE_SIZE);
        GameManager.instance.Flow();
    }

    public void removeNuclearWaste() {
        if (pileSize > 0) {
            pileSize--;
            GameManager.instance.Flow();
        }
    }
}
}