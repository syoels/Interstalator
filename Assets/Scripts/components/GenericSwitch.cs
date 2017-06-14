using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public abstract class GenericSwitch : AltShipComponent {

    protected abstract ElementTypes[] SwitchType { get; }
    [SerializeField] private float[] distribution;

    /// <summary>
    /// Switches only transfer one element type to their children 
    /// </summary>
    protected override void SetRequiredInputs() {
        AddRequiredInput(SwitchType);
        InitDistribution();
    }

    protected void InitDistribution(){
        if (distribution.Length != children.Length) {
            distribution = new float[children.Length];
            float division = 1f / children.Length;
            for (int i = 0; i < children.Length; i++) {
                distribution[i] = division;
            }
        }
        ApplyDistribution(distribution);
    }

    public void ApplyDistribution(float[] newDistribution) {
        Debug.Assert(newDistribution.Length == children.Length);
        distribution = newDistribution;
        // Used to avoid referenceing null object at the start of the game
        if (GameManager.instance != null) {
            GameManager.instance.Flow();
        }
    }

    protected override List<Output> InnerProcess() {
        List<Output> outputs = new List<Output>();
        ElementTypes passType = (ElementTypes)incoming[0].type;
        for (int i = 0; i < children.Length; i++) {
            ShipComponent child = children[i];
            Output t = new Output(child, passType, distribution[i] * incoming[0].amount);
            outputs.Add(t);
        }

        string textualDistribution = "";
        foreach (float percentage in distribution) {
            textualDistribution += (int)(percentage * 100) + "% - ";
        }
        SetStatus("Distributing " + incoming[0].amount + " " + passType + " by " +
            // Removing the extra ' - ' from the distribution text
            textualDistribution.Substring(0, textualDistribution.Length - 3));
        return outputs;
    }

    override public bool IsInteractable() {
        return true;
    }

    override public void Interact() {
        GameManager.instance.switchController.BringUpSlider(
            distribution,
            this,
            true
        );
    }

    override public string InteractionDescription {
        get {
            return "Change " + (ElementTypes)incoming[0].type + " levels";
        }
    }

}
}

