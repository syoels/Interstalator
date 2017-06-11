using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Interstalator {
/// <summary>
/// Manages item holding, dropping and displaying. Only relevant for textual game since
/// player script should be in charge of these.
/// </summary>
public class ItemManager : MonoBehaviour {
    public GameObject ItemPanel;

    private Text itemText;
    private GameObject dropButton;

    private TextualItem _heldItem;

    /// <summary>
    /// Gets or sets the held item. If None is received instead of a TextualItem -
    /// sets the current item to non.
    /// </summary>
    /// <value>The held item.</value>
    public TextualItem heldItem {
        get {
            return _heldItem;
        }
        set {
            _heldItem = value;
            if (_heldItem == null) {
                itemText.text = "None";
                dropButton.SetActive(false);
            } else {
                itemText.text = _heldItem.name;
                _heldItem.gameObject.SetActive(false);
                dropButton.SetActive(true);
            }
        }
    }

    void Awake() {
        itemText = ItemPanel.transform.Find("ItemName").GetComponent<Text>();
        dropButton = ItemPanel.transform.Find("Drop").gameObject;
        // Could be set via editor but this makes sure the button works
        Button dropButtonScript = dropButton.GetComponent<Button>();
        dropButtonScript.onClick.AddListener(DropItem);
    }

    /// <summary>
    /// Returns the type of the currently held item - IsInteractable checks should use this
    /// </summary>
    /// <value>The type of the held item.</value>
    public ItemType heldItemType {
        get {
            if (_heldItem != null) {
                return _heldItem.itemType;
            }
            return ItemType.None;
        }
    }

    /// <summary>
    /// Drops the currently held item - should not be activated when no items are held
    /// </summary>
    public void DropItem() {
        // Can't drop an item if not holding anything
        Debug.Assert(heldItem != null);
        TextualItem item = heldItem;
        heldItem = null;
        item.gameObject.SetActive(true);
        return;
    }
}
}