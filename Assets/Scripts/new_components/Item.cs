using UnityEngine;
using System.Collections;

namespace Interstalator {
public enum ItemType {
    // Names have underscore to easily print them in human readable format
    None, Water_Hoze, Tape, Nuclear_Waste
}

public abstract class Item : MonoBehaviour, Interactable {
    public ItemType type;

    public override string ToString() {
        return type.ToString().Replace("_", " ");
    }

    public bool IsInteractable() {
        return GameManager.instance.itemManager.heldItemType == ItemType.None;
    }

    public void Interact() {
        GameManager.instance.itemManager.heldItem = this;
    }

    public string GetInteractionText() {
        return "Pick up " + this;
    }

    // Graphical items should override this
    virtual public void SetGlow(bool isGlowing) {
        return;
    }
}
}