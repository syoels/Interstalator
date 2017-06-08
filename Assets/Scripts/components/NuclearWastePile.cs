using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public class NuclearWastePile : ShipComponent {
    public int pileSize;
    private const int MAX_PILE_SIZE = 8;
    private float lastAddTime = 0f;
    private float wasteRatio = 0f;

    void Update() {
        if (wasteRatio > 0 && pileSize < MAX_PILE_SIZE) {
            // How many seconds to wait before increasing pile size
            float pileIncreaseRate = 1f / wasteRatio;
            if (Time.time - lastAddTime > pileIncreaseRate) {
                pileSize = Mathf.Min(pileSize + 1, MAX_PILE_SIZE);
                lastAddTime = Time.time;
                GraphManager.instance.Flow();
            }
        }
    }

    protected override void SetRequiredInputs() {
        AddRequiredInput(ElementTypes.Water, ElementTypes.Poison);
        AddRequiredInput(ElementTypes.WastePerSecond);
    }

    protected override List<Output> InnerProcess() {
        wasteRatio = incoming[1].amount;
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