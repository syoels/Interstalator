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

    #region IPointer implementations

    public void OnPointerEnter(PointerEventData eventData) {
        graphic.color = Color.magenta;
    }

    public void OnPointerExit(PointerEventData eventData) {
        graphic.color = baseColor;
    }

    public void OnPointerClick(PointerEventData eventData) {
        graphic.color = baseColor;
        GraphManager.instance.GrabItem(this);
    }

    #endregion
}
}