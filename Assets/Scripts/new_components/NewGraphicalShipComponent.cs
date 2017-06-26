using UnityEngine;
using System.Collections;

namespace Interstalator {
[RequireComponent(Animator)]
public abstract class NewGraphicalShipComponent : NewShipComponent {
    const string WAIT_TAG = "Wait";
    const float DEFAULT_WAIT_TIME = 0.5f;
    protected Animator animator;
    private int baseLayerIndex;

    void Awake() {
        base.Awake();
        animator = GetComponent<Animator>();
        baseLayerIndex = animator.GetLayerIndex("Base Layer");
    }

    protected override float GetProcessingDelay() {
        AnimatorStateInfo currState = animator.GetNextAnimatorStateInfo(baseLayerIndex);

        bool requiresWait = currState.IsTag(WAIT_TAG);
        if (!requiresWait) {
            return 0f;
        }

        AnimatorClipInfo[] currPlayingClips = animator.GetCurrentAnimatorClipInfo(baseLayerIndex);
        if (currPlayingClips.Length == 0) { return DEFAULT_WAIT_TIME; }
        // Used to wait for animation that allready started to wait
        float clipLength = currPlayingClips[0].clip.length;
        float stateElapsedTime = currState.length;
        float remainingTime = clipLength - (stateElapsedTime % clipLength);
        return remainingTime;
    }
}
}