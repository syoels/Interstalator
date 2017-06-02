using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Interstalator {
public class ItemDisplayController : MonoBehaviour {
    public GameObject ItemPanel;
    public GameObject InteractionPanel;

    private Text itemText;
    private Text interactionText;

    void Awake() {
        itemText = ItemPanel.transform.Find("ItemName").GetComponent<Text>();
        interactionText = InteractionPanel.transform.Find("Interaction").GetComponent<Text>();
    }

    /// <summary>
    /// Write the name of the currently held item
    /// </summary>
    /// <param name="item">Item name</param>
    public void SetItem(string item) {
        if (item == null) {
            itemText.text = "None";
        } else {
            itemText.text = item;
        }
    }

    /// <summary>
    /// Write the description of the currently available interaction
    /// </summary>
    /// <param name="interaction">Interaction description</param>
    public void SetInteraction(string interaction) {
        if (interaction == null) {
            interactionText.text = "None";
        } else {
            interactionText.text = interaction;
        }
    }
}
}