using UnityEngine;
using System.Collections;

namespace Interstalator {
public class InteractableTextualWasteDisposal : InteractableComponent {
    public override bool IsInteractable() {
        return GameManager.instance.itemManager.heldItemType == ItemType.Nuclear_Waste;
    }

    public override void Interact() {
        GameObject waste = GameManager.instance.itemManager.heldItem.gameObject;
        GameManager.instance.itemManager.DropItem();
        Destroy(waste);
    }

    public override string GetInteractionText() {
        return "Dispose of the held waste";
    }
}
}