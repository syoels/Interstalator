using UnityEngine;
using System.Collections;

namespace Interstalator {
public interface Interactable {
    bool IsInteractable();
    void Interact();
    string GetInteractionText();
    void SetGlow(bool isGlowing);
}
}