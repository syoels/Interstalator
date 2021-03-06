﻿using System;
using UnityEngine;

namespace Interstalator {
public class NewGraphicalGenericLever : NewGraphicalShipComponent, Toggleable {

    public ElementTypes[] possibleTypes;
    public AudioClip leverTurnSound;
    private int currInputParamId;
    private AudioSource leverAudio;
    [SerializeField] private int currInput = 0; 

    new void Awake() {
        base.Awake();
        leverAudio = GetComponent<AudioSource>();
        leverAudio.clip = leverTurnSound;
    }

    protected override ElementTypes[][] DefineInputs() {
        return new ElementTypes[2][] { possibleTypes, possibleTypes };
    }

    protected override void SetAnimationParameterIds() {
        currInputParamId = Animator.StringToHash("Input");
        animator.SetInteger(currInputParamId, currInput);
    }

    protected override NewShipComponentOutput[] InnerProcess() {

        float amount = inputs[currInput].amount;
        ElementTypes type = inputs[currInput].type;
        return DistributeAmongChildren(type, amount);
    }

    public void Toggle() {
        currInput = (currInput + 1) % inputs.Length;
        animator.SetInteger(currInputParamId, currInput);
        leverAudio.Play();
        GameManager.instance.Flow();
    }
}
}
