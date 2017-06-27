using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Interstalator {
public class GraphicalItemManager : ItemManager {

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
                _heldItem.transform.SetParent(GameManager.instance.player.transform);
//                _heldItem.gameObject.SetActive(false);
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