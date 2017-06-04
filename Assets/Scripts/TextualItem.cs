using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Interstalator {
public enum ItemType {
    WaterHoze, Tape, NuclearWaste
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
    }

    #endregion
}
}