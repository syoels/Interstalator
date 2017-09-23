using System;
using UnityEngine;

namespace Interstalator {
public class NewGraphicalGenericGenerator : NewGraphicalShipComponent, Toggleable {

    public float amount;
    public ElementTypes type;
    public AudioClip onSound;
    public AudioClip offSound;
    // Might add another audio source for toggle so this will not be set in Awake
    public AudioSource generatorAudio;

    protected int amountParamId;
    protected int elementParamId;
    protected bool isWorking = true;

    protected override void SetAnimationParameterIds() {
        amountParamId = Animator.StringToHash("Amount");
        elementParamId = Animator.StringToHash("Element");
    }

    protected override NewShipComponentOutput[] InnerProcess() {
        float generatedAmount = isWorking ? amount : 0f;
        animator.SetFloat(amountParamId, generatedAmount);
        animator.SetInteger(elementParamId, animatorElements[type]);
        return DistributeAmongChildren(type, generatedAmount);
    }

    public void Toggle() {
        isWorking = !isWorking;
        if (generatorAudio != null) {
            generatorAudio.Stop();
            if (isWorking && onSound != null) {
                generatorAudio.clip = onSound;
                generatorAudio.loop = true;
                generatorAudio.Play();
            } else if (!isWorking && offSound != null) {
                generatorAudio.clip = offSound;
                generatorAudio.loop = true;
                generatorAudio.Play();
            }
        }
        GameManager.instance.Flow();
    }

    public void SetAmount(float newAmount) {
        amount = newAmount; 
        GameManager.instance.Flow();
    }

}
}

