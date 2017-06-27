using UnityEngine;
using System.Collections;

namespace Interstalator {
[RequireComponent(typeof (NewGraphicalWastePile))]
public class InteractableGraphicalWastePile : InteractableComponent {
    public GameObject wastePrefab;

    private NewGraphicalWastePile wastePile {
        get { return (NewGraphicalWastePile)relComponent; }
    }

    public override bool IsInteractable() {
        ItemType heldItemType = GameManager.instance.heldItemType;
        if (heldItemType == ItemType.Nuclear_Waste) {
            return true;
        }
        return heldItemType == ItemType.None && wastePile.pileSize > 0;
    }

    public override void Interact() {
        if (GameManager.instance.heldItemType == ItemType.Nuclear_Waste) {
            wastePile.pileSize++;
            Item currentWaste = GameManager.instance.itemManager.heldItem;
            GameManager.instance.itemManager.DropItem();
            Destroy(currentWaste.gameObject);
        } else {
            wastePile.pileSize--;
            GameObject waste = Instantiate(wastePrefab);
            GameManager.instance.itemManager.heldItem = waste.GetComponent<Item>();
        }
    }

    public override string GetInteractionText() {
        if (GameManager.instance.heldItemType == ItemType.Nuclear_Waste) {
            return "Add waste to pile";
        }
        return "Pick nuclear waste from pile";
    }
}
}