using UnityEngine;
using System.Collections;

namespace Interstalator {
public class NewTextualGenericSwitch : NewTextualShipComponent {
    [SerializeField] private ElementTypes[] switchElements;
    [SerializeField] private float[] distribution;

    protected override ElementTypes[][] DefineInputs() {
        return new ElementTypes[][] { switchElements };
    }

    private void InitDistribution(){
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


    protected override NewShipComponentOutput[] InnerProcess() {
        ElementTypes passType = inputs[0].type;
        float amount = inputs[0].amount;

        for (int i = 0; i < outputs.Length; i++) {
            NewShipComponentOutput output = outputs[i];
            output.Set(passType, distribution[i] * inputs[0].amount);
        }

        string textualDistribution = "";
        foreach (float percentage in distribution) {
            textualDistribution += (int)(percentage * 100) + "% - ";
        }
        SetStatus("Distributing " + amount + " " + passType + " by " +
            // Removing the extra ' - ' from the distribution text
            textualDistribution.Substring(0, textualDistribution.Length - 3));
        return outputs;
    }
}
}