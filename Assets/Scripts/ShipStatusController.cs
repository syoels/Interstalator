using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Interstalator {
public class ShipStatusController : MonoBehaviour {
    public enum ShipSystem { Air, Engine, Gravity }

    [Tooltip("UI panel under the canvas should be referenced here")]
    public GameObject statusPanel;

    private float startTime;
    private Dictionary<ShipSystem, Text> systemTexts = new Dictionary<ShipSystem, Text>();

    void Awake() {
        ShipSystem[] systems = (ShipSystem[])System.Enum.GetValues(typeof(ShipSystem));
        foreach (ShipSystem value in systems) {
            systemTexts[value] = statusPanel.transform.FindChild(value.ToString()).GetComponent<Text>();
        }
    }

    /// <summary>
    /// Set text value for specific system. Adds the system name as prefix 
    /// </summary>
    private void SetSystemText(ShipSystem system, string text) {
        systemTexts[system].text = system.ToString() + " - " + text;
    }

    /// <summary>
    /// Set color value for the system's text line
    /// </summary>
    private void SetSystemColor(ShipSystem system, Color color) {
        systemTexts[system].color = color;
    }

    /// <summary>
    /// Marks the given system as OK
    /// </summary>
    /// <param name="system">System.</param>
    public void SetOk(ShipSystem system) {
        SetSystemText(system, "OK");
        SetSystemColor(system, Color.green);
    }

    /// <summary>
    /// Writes a problem for the given system and marks it as problematic.
    /// </summary>
    public void SetProblem(ShipSystem system, string text) {
        SetSystemText(system, text);
        SetSystemColor(system, Color.red);
    }
}
}