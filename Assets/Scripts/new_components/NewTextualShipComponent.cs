using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Interstalator {
public abstract class NewTextualShipComponent : NewShipComponent {
    private const int STATUS_LINE_SIZE = 17;
    private const float DEFAULT_DELAY = 1f;

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

    protected override float GetProcessingDelay() {
        if (stateChanged) {
            return DEFAULT_DELAY;
        }
        return 0f;
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