using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Interstalator {
public enum ItemType {
    // Names have underscore to easily print them in human readable format
    None, Water_Hoze, Tape, Nuclear_Waste
}
public class TextualItem : MonoBehaviour,
                           IPointerEnterHandler,
                           IPointerExitHandler,
                           IPointerClickHandler {
    public ItemType itemType;

    private SpriteRenderer graphic;
    private Color baseColor;

    void Awake() {
        graphic = transform.Find("Graphic").GetComponent<SpriteRenderer>();
        baseColor = graphic.color;
    }

    private bool CanInteract() {
        return GameManager.instance.itemManager.heldItemType == ItemType.None;
    }

    #region IPointer implementations

    public void OnPointerEnter(PointerEventData eventData) {
        if (CanInteract()) {
            graphic.color = Color.magenta;
            GameManager.instance.interactionDisplay.Set("Pick up " + this.itemType);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        graphic.color = baseColor;
        GameManager.instance.interactionDisplay.Clear();
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (CanInteract()) {
            graphic.color = baseColor;
            GameManager.instance.itemManager.heldItem = this;
        }
    }

    #endregion
}
}