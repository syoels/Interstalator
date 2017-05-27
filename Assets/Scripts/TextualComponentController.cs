using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public class TextualComponentController : MonoBehaviour {
    public string name;

    private Color baseColor;

    private TextMesh statusText;
    private TextMesh nameText;
    private SpriteRenderer graphic;

    void Awake() {
        graphic = transform.FindChild("Graphic").GetComponent<SpriteRenderer>();
        nameText = transform.FindChild("Name").GetComponent<TextMesh>();
        statusText = transform.FindChild("Status").GetComponent<TextMesh>();

        nameText.text = name;
        baseColor = graphic.color;
    }

    public void SetStatus(string status) {
        Debug.Log("Got new status: " + status);
        statusText.text = status;
    }

    void OnMouseEnter() {
        graphic.color = Color.yellow;
    }

    void OnMouseExit() {
        graphic.color = baseColor;
    }

}
}