using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Interstalator {
/// <summary>
/// In charge of displaying the available action. Tells the player what will interacting
/// with something do.
/// </summary>
public class InteractionDisplay : MonoBehaviour {
    public GameObject InteractionPanel;

    private Text interactionText;

    void Awake() {
        interactionText = InteractionPanel.transform.Find("Interaction").GetComponent<Text>();
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