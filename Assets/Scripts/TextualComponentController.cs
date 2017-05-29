using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Interstalator {
public class TextualComponentController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public string componentName;

    private Color baseColor;

    private TextMesh statusText;
    private TextMesh nameText;
    private SpriteRenderer graphic;

    void Awake() {
        graphic = transform.FindChild("Graphic").GetComponent<SpriteRenderer>();
        nameText = transform.FindChild("Name").GetComponent<TextMesh>();
        statusText = transform.FindChild("Status").GetComponent<TextMesh>();

        nameText.text = componentName;
        baseColor = graphic.color;
    }

    public void SetStatus(string status) {
        Debug.Log("Got new status: " + status);
        statusText.text = status;
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