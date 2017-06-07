using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public class NuclearWastePile : ShipComponent {
    public int pileSize;
    private const int MAX_PILE_SIZE = 8;

    protected override void SetRequiredInputs() {
        AddRequiredInput(ElementTypes.Water, ElementTypes.Poison);
        AddRequiredInput(ElementTypes.NuclearWasteRatio);
    }

    protected override List<Output> InnerProcess() {
        // As time goes by more and more nuclear waste should be sent here
        pileSize = Mathf.Min(pileSize + (int)(incoming[1].amount), MAX_PILE_SIZE);
        List<Output> outputs = new List<Output>();

        float distribution = incoming[0].amount / children.Length;

        // Sends water or poison to all of its children
        foreach (ShipComponent child in children) {
            if (pileSize > 0) {
                outputs.Add(new Output(children[0], ElementTypes.Poison, distribution));
            } else {
                outputs.Add(new Output(children[0], (ElementTypes)incoming[0].type, distribution));
            }
        }
        
        SetStatus("Pile size: " + pileSize);
        return outputs;
    }

    protected override string ComponentName {
        get {
            return "Nuclear Waste Pile";
        }
    }
    
}
}