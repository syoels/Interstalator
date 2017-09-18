using UnityEngine;
using System.Collections;

namespace Interstalator {
public enum ItemType {
    // Names have underscore to easily print them in human readable format
    None, Water_Hoze, Tape, Nuclear_Waste
}

public abstract class Item : MonoBehaviour, Interactable {
    public ItemType type;
    public AudioClip pickupSound;
    public AudioClip dropSound;
    private AudioSource itemAudio;

    void Start() {
        itemAudio = GetComponent<AudioSource>();
    }

    public override string ToString() {
        return type.ToString().Replace("_", " ");
    }

    private bool IsHeld() {
        return GameManager.instance.itemManager.heldItem == this;
    }

    public bool IsInteractable() {
        return GameManager.instance.itemManager.heldItemType == ItemType.None || IsHeld();
    }

    public void Interact() {
        if (IsHeld()) {
            GameManager.instance.itemManager.DropItem();
            itemAudio.clip = dropSound;
            itemAudio.Play();
            return;
        }

        itemAudio.clip = pickupSound;
        itemAudio.Play();
        GameManager.instance.itemManager.heldItem = this;
    }

    public string GetInteractionText() {
        if (IsHeld()) {
            return "Drop " + this;
        }
        return "Pick up " + this;
    }

    // Graphical items should override this
    virtual public void SetGlow(bool isGlowing) {
        return;
    }
}
}