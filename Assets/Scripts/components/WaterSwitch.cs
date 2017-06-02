using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public class WaterSwitch : ShipComponent {

    [SerializeField] private float[] distribution;
    public SwitchParametersController switchController;

    protected override string ComponentName {
        get {
            return "Water Switch";
        }
    }

    protected override void SetRequiredInputs() {
        AddRequiredInput(ElementTypes.Water);

        // TODO: Create another method that runs in the start procedure for
        // these kinds of things
        if (distribution.Length == 0) {
            distribution = new float[children.Length];
            float division = 1f / children.Length;
            for (int i = 0; i < children.Length; i++) {
                distribution[i] = division;
            }
        }
        // Used to make sure the user didn't create a wrong distribution
        ApplyDistribution(distribution);
    }


    protected override List<Output> InnerProcess() {
        List<Output> outputs = new List<Output>();
        for (int i = 0; i < children.Length; i++) {
            ShipComponent child = children[i];
            Output t = new Output(child, ElementTypes.Water, distribution[i]);
            outputs.Add(t);
        }

        string textualDistribution = "";
        foreach (float percentage in distribution) {
            textualDistribution += (int)(percentage * 100) + "% - ";
        }
        SetStatus("Distributing " + incoming[0].amount + " Water by " +
            textualDistribution.Substring(0, textualDistribution.Length - 3));
        return outputs;
    }

    public void ApplyDistribution(float[] newDistribution) {
        Debug.Assert(newDistribution.Length == children.Length);
        distribution = newDistribution;
        // Used to avoid referenceing null object at the start of the game
        if (GraphManager.instance != null) {
            GraphManager.instance.Flow();
        }
    }

    override public bool IsInteractable() {
        return true;
    }

    override public void Interact() {
        switchController.BringUpSlider(
            distribution,
            this,
            true
        );
    }

    override public string InteractionDescription {
        get {
            return "Change water levels";
        }
    }
}
}