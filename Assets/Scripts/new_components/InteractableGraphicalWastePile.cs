using UnityEngine;
using System.Collections;

namespace Interstalator {
[RequireComponent(typeof (NewGraphicalWastePile))]
public class InteractableGraphicalWastePile : InteractableComponent {

    private NewGraphicalWastePile wastePile {
        get { return (NewGraphicalWastePile)relComponent; }
    }

    public override bool IsInteractable() {
        return wastePile.pileSize > 0;
    }

    public override void Interact() {
        wastePile.pileSize--;
    }

    public override string GetInteractionText() {
        return "Pick nuclear waste from pile";
    }
}
}