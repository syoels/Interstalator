using UnityEngine;
using System.Collections;

namespace Interstalator {
[RequireComponent(typeof (NewTextualNuclearWastePile))]
public class InteractableTextualWastePile : Interactable {
    public GameObject wastePrefab;

    // Used to place the nuclear waste on the map
    private Vector2 wastePosition = new Vector2(23.5f, -9.6f);
    private const float MAX_WASTE_Y = 2.4f;

    private NewTextualNuclearWastePile wastePile {
        get { return (NewTextualNuclearWastePile)relComponent; }
    }

    public override bool IsInteractable() {
        return GameManager.instance.itemManager.heldItemType == ItemType.None && wastePile.pileSize > 0;
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
        wastePile.pileSize--;
        GameManager.instance.Flow();
        GameManager.instance.itemManager.heldItem = waste.GetComponent<TextualItem>();
    }

    public override string GetInteractionText() {
        return "Pick up nuclear waste";
    }


}
}