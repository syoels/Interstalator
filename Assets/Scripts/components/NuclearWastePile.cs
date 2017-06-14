using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public class NuclearWastePile : AltShipComponent {
    public int pileSize;
    public GameObject wastePrefab;
    private const int MAX_PILE_SIZE = 8;
    private float lastAddTime = 0f;
    private float wasteRatio = 0f;

    // Used to place the nuclear waste on the map
    // - used only in textual component and should be deleted
    private Vector2 wastePosition = new Vector2(23.5f, -9.6f);
    private const float MAX_WASTE_Y = 2.4f;

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

    public override bool IsInteractable() {
        return GameManager.instance.itemManager.heldItemType == ItemType.None && pileSize > 0;
    }

    public override void Interact() {
        GameObject waste = Instantiate(
                               wastePrefab,
                               GameManager.instance.itemManager.itemsContainer
                           );
        waste.transform.position = wastePosition;
        wastePosition.y += 2;
        // Resets the waste drop position (may create objects on top of each other
        // but the player should realise what's happening by this point
        if (wastePosition.y > MAX_WASTE_Y) {
            wastePosition.y = -9.6f;
        }
        pileSize--;
        GameManager.instance.Flow();
        GameManager.instance.itemManager.heldItem = waste.GetComponent<TextualItem>();
    }

    public override string InteractionDescription {
        get {
            return "Pick up nuclear waste";
        }
    }
}
}