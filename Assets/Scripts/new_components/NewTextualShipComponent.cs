﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Interstalator {
public abstract class NewTextualShipComponent : NewShipComponent {
    private const int STATUS_LINE_SIZE = 17;

    private Color baseColor;

    private TextMesh statusText;
    private SpriteRenderer graphic;

    new void Awake() {
        base.Awake();
        graphic = transform.FindChild("Graphic").GetComponent<SpriteRenderer>();
        statusText = transform.FindChild("Status").GetComponent<TextMesh>();
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

    override public void SetGlow(bool isGlowing) {
        if (isGlowing) {
            graphic.color = Color.yellow;
        } else {
            graphic.color = baseColor;
        }
    }

}
}