using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Interstalator {
[RequireComponent(typeof (Animator))]
    public abstract class NewGraphicalShipComponent : NewShipComponent {
    const string WAIT_TAG = "Wait";
    const float DEFAULT_WAIT_TIME = 0.5f;

    protected Animator animator;
    protected Dictionary<ElementTypes, int> animatorElements; // Used to map between element enums and animator params.
    private int baseLayerIndex;
    private Transform glowObj;

    new void Awake() {
        base.Awake();
        animatorElements = new Dictionary<ElementTypes, int>(){
            {ElementTypes.None, 0}, 
            {ElementTypes.Electricity, 1}, 
            {ElementTypes.Water, 2}, 
            {ElementTypes.Poison, 3}, 
            {ElementTypes.Air, 4},
            {ElementTypes.PoisonousAir, 5}
        };
        animator = GetComponent<Animator>();
        SetAnimationParameterIds();
        baseLayerIndex = animator.GetLayerIndex("Base Layer");
        glowObj = transform.Find("Glow");
    }

    protected virtual void SetAnimationParameterIds() {}

    protected override float GetProcessingDelay() {
        // Used in initial flow to get everything working fast.
        // TODO: Either remove this or bring it to parent class
        if (!stateChanged) {
            return 0f;
        }
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

    public override void SetGlow(bool isGlowing) {
        if (glowObj != null) {
            glowObj.gameObject.SetActive(isGlowing);
        }
    }
}
}