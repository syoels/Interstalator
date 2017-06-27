using UnityEngine;
using System.Collections;

namespace Interstalator {
public class GraphicalItem : Item {
    private Transform glowObj;

    void Awake() {
        glowObj = transform.Find("Glow");
    }

    override public void SetGlow(bool isGlowing) {
        if (glowObj != null) {
            glowObj.gameObject.SetActive(isGlowing);
        }
    }
}
}