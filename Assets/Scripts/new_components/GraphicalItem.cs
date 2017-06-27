using UnityEngine;
using System.Collections;

namespace Interstalator {
public class GraphicalItem : Item {
    private Transform glowObj;
    private SpriteRenderer sprite;

    void Awake() {
        glowObj = transform.Find("Glow");
        sprite = transform.Find("Graphics").GetComponent<SpriteRenderer>();
    }

    override public void SetGlow(bool isGlowing) {
        if (glowObj != null) {
            glowObj.gameObject.SetActive(isGlowing);
        }
    }

    // Used to place item in different graphics layer
    public SpriteRenderer GetSprite() {
        return sprite;
    }
}
}