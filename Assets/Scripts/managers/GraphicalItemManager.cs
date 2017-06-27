using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Interstalator {
public class GraphicalItemManager : ItemManager {
    private Transform playerGraphics;

    void Start() {
        playerGraphics = GameManager.instance.player.transform.Find("Graphics");
    }

    override public Item heldItem {
        get {
            return _heldItem;
        }
        set {
            _heldItem = value;
            if (_heldItem == null) {
                SetItemText("None");
            } else {
                SetItemText(_heldItem.ToString());
                _heldItem.transform.SetParent(playerGraphics);
                // Place item in the middle of the player
                _heldItem.transform.localPosition = new Vector2(0f, 0f);
                SpriteRenderer sprite = ((GraphicalItem)_heldItem).GetSprite();
                sprite.sortingLayerName = "Foreground";
                sprite.sortingOrder = -1;
            }
        }
    }

    override public void DropItem() {
        // TODO: Write graphical logic
        // Can't drop an item if not holding anything
        Debug.Assert(heldItem != null);
//        TextualItem item = heldItem;
//        heldItem = null;
//        item.gameObject.SetActive(true);
        return;
    }
}
}