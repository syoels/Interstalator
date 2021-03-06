﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Interstalator {
public class TextualComponentController : MonoBehaviour,
                                          IPointerEnterHandler,
                                          IPointerExitHandler,
                                          IPointerClickHandler {
    private const int STATUS_LINE_SIZE = 17;

    private Color baseColor;

    private TextMesh statusText;
    private TextMesh nameText;
    private SpriteRenderer graphic;

    private ShipComponent relComponent;

    void Awake() {
        graphic = transform.Find("Graphic").GetComponent<SpriteRenderer>();
        nameText = transform.Find("Name").GetComponent<TextMesh>();
        statusText = transform.Find("Status").GetComponent<TextMesh>();
        relComponent = GetComponent<ShipComponent>();

        baseColor = graphic.color;
    }

    public void SetStatus(string status) {
        if (status.Length > STATUS_LINE_SIZE) {
            string[] words = status.Split(' ');
            List<string> newStatus = new List<string>();
            newStatus.Add("");
            foreach (string word in words) {
                int lineIndex = newStatus.Count - 1;
                if (newStatus[lineIndex] == "") {
                    newStatus[lineIndex] = word;
                } else if ((newStatus[lineIndex] + word).Length > STATUS_LINE_SIZE) {
                    newStatus.Add(word);
                } else {
                    newStatus[lineIndex] += " " + word;
                }
            }
            status = string.Join("\n", newStatus.ToArray());
        }
        statusText.text = status;
    }

    public void SetName(string name) {
        nameText.text = name;
    }

    #region IPointer handlers
    public void OnPointerEnter(PointerEventData eventData) {
        if (relComponent.IsInteractable()) {
            graphic.color = Color.yellow;
            GameManager.instance.interactionDisplay.Set(relComponent.InteractionDescription);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        graphic.color = baseColor;
        GameManager.instance.interactionDisplay.Clear();
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) {
            if (relComponent.IsInteractable()) {
                relComponent.Interact();
            }
        }
    }
    #endregion

}
}