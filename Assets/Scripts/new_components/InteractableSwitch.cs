using UnityEngine;
using System.Collections;

namespace Interstalator {
public class InteractableSwitch : InteractableComponent {
    public override bool IsInteractable() {
        return GameManager.instance.itemManager.heldItemType == ItemType.None;
    }

    public override void Interact() {
        SwitchCaller relSwitch = (SwitchCaller)relComponent;
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