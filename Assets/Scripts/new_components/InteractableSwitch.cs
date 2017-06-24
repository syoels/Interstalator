using UnityEngine;
using System.Collections;

namespace Interstalator {
public class InteractableSwitch : Interactable {
    public override bool IsInteractable() {
        return true;
    }

    public override void Interact() {
        NewTextualGenericSwitch relSwitch = (NewTextualGenericSwitch)relComponent;
        GameManager.instance.switchController.BringUpSlider(
            relSwitch.distribution,
            relSwitch,
            true
        );
    }

    public override string GetInteractionText() {
        return "Change distribution levels";
    }
}
}