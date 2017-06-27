using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Interstalator {
/// <summary>
/// Manages item holding, dropping and displaying. Only relevant for textual game since
/// player script should be in charge of these.
/// </summary>
public class TextualItemManager : ItemManager {
    private GameObject dropButton;

    /// <summary>
    /// Gets or sets the held item. If None is received instead of a TextualItem -
    /// sets the current item to non.
    /// </summary>
    /// <value>The held item.</value>
    override public Item heldItem {
        get {
            return _heldItem;
        }
        set {
            _heldItem = value;
            if (_heldItem == null) {
                SetItemText("None");
                dropButton.SetActive(false);
            } else {
                SetItemText(_heldItem.ToString());
                _heldItem.gameObject.SetActive(false);
                dropButton.SetActive(true);
                GameManager.instance.interactionDisplay.Set("None");
            }
        }
    }

    new void Awake() {
        base.Awake();
        dropButton = itemPanel.transform.Find("Drop").gameObject;
        // Could be set via editor but this makes sure the button works
        Button dropButtonScript = dropButton.GetComponent<Button>();
        dropButtonScript.onClick.AddListener(DropItem);
    }

    /// <summary>
    /// Drops the currently held item - should not be activated when no items are held
    /// </summary>
    override public void DropItem() {
        // Can't drop an item if not holding anything
        Debug.Assert(heldItem != null);
        Item item = heldItem;
        heldItem = null;
        item.gameObject.SetActive(true);
        return;
    }
}
}