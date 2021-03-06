﻿using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Interstalator {
public abstract class InteractableComponent : MonoBehaviour,
                                     Interactable,
                                     IPointerEnterHandler,
                                     IPointerExitHandler,
                                     IPointerClickHandler {

    protected NewShipComponent relComponent;
    private bool isTextual;

    void Awake() {
        relComponent = GetComponent<NewShipComponent>();
        isTextual = (relComponent is NewTextualShipComponent);
    }

    abstract public bool IsInteractable();

    abstract public void Interact();

    abstract public string GetInteractionText();

    // Here to allow player controller to decide on component glowing
    public void SetGlow(bool isGlowing) {
        relComponent.SetGlow(isGlowing);
    }

    #region IPointer handlers
    public void OnPointerEnter(PointerEventData eventData) {
        // Avoid mouse clicking on graphical components
        if (!isTextual) {
            return;
        }

        if (IsInteractable()) {
            relComponent.SetGlow(true);
            GameManager.instance.interactionDisplay.Set(GetInteractionText());
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (!isTextual) {
            return;
        }
        relComponent.SetGlow(false);
        GameManager.instance.interactionDisplay.Clear();
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (!isTextual) {
            return;
        }
        if (eventData.button == PointerEventData.InputButton.Left) {
            if (IsInteractable()) {
                Interact();
            }
        }
    }
    #endregion

}
}