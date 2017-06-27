using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Interstalator {
public abstract class ItemManager : MonoBehaviour {
    public GameObject itemPanel;
    [Tooltip("Used for convinience to place all items in same object in inspector")]
    public Transform itemsContainer;

    private Text itemText;
    protected Item _heldItem;
    abstract public Item heldItem { get; set; }

    protected void Awake() {
        itemText = itemPanel.transform.Find("ItemName").GetComponent<Text>();
    }

    protected void SetItemText(string text) {
        itemText.text = text;
    }

    public ItemType heldItemType {
        get {
            if (_heldItem != null) {
                return _heldItem.type;
            }
            return ItemType.None;
        }
    }

    abstract public void DropItem();
}
}