using UnityEngine;
using System.Collections;

namespace Interstalator {
public interface Toggleable {
    void Toggle();
}

public class InteractableToggle : InteractableComponent {
    public string description;

    public override void Interact() {
        ((Toggleable)relComponent).Toggle();
    }

    public override bool IsInteractable() {
        return GameManager.instance.itemManager.heldItemType == ItemType.None;
    }

    public override string GetInteractionText() {
        return description;
    }
}
}