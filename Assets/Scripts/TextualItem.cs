using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Interstalator {
public class TextualItem : Item,
                           IPointerEnterHandler,
                           IPointerExitHandler,
                           IPointerClickHandler {
    private SpriteRenderer graphic;
    private Color baseColor;

    void Awake() {
        graphic = transform.Find("Graphic").GetComponent<SpriteRenderer>();
        baseColor = graphic.color;
    }

    #region IPointer implementations

    public void OnPointerEnter(PointerEventData eventData) {
        if (IsInteractable()) {
            graphic.color = Color.magenta;
            GameManager.instance.interactionDisplay.Set("Pick up " + this);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        graphic.color = baseColor;
        GameManager.instance.interactionDisplay.Clear();
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (IsInteractable()) {
            graphic.color = baseColor;
            Interact();
        }
    }

    #endregion
}
}