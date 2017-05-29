using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Interstalator {
public class TextualComponentController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    private Color baseColor;

    private TextMesh statusText;
    private TextMesh nameText;
    private SpriteRenderer graphic;

    void Awake() {
        graphic = transform.FindChild("Graphic").GetComponent<SpriteRenderer>();
        nameText = transform.FindChild("Name").GetComponent<TextMesh>();
        statusText = transform.FindChild("Status").GetComponent<TextMesh>();

        baseColor = graphic.color;
    }

    public void SetStatus(string status) {
        Debug.Log("Got new status: " + status);
        statusText.text = status;
    }

    public void SetName(string name) {
        nameText.text = name;
    }

    #region IPointer handlers
    public void OnPointerEnter(PointerEventData eventData) {
        graphic.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData) {
        graphic.color = baseColor;
    }
    #endregion

}
}