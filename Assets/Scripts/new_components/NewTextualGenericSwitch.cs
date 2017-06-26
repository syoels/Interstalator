using UnityEngine;
using System.Collections;

namespace Interstalator {
public class NewTextualGenericSwitch : NewTextualShipComponent, SwitchCaller {
    [SerializeField] private ElementTypes[] switchElements;
    [SerializeField] private float[] _distribution;
    public float[] distribution {
        get {
            return _distribution;
        }
        set {
            Debug.Assert(value.Length == children.Length);
            _distribution = value;
            // Used to avoid referenceing null object at the start of the game
            if (GameManager.instance != null) {
                GameManager.instance.Flow();
            }
        }
    }

    protected override ElementTypes[][] DefineInputs() {
        InitDistribution();
        return new ElementTypes[1][] { switchElements };
    }

    private void InitDistribution(){
        if (_distribution.Length != children.Length) {
            _distribution = new float[children.Length];
            float division = 1f / children.Length;
            for (int i = 0; i < children.Length; i++) {
                _distribution[i] = division;
            }
        }
    }


    protected override NewShipComponentOutput[] InnerProcess() {
        ElementTypes passType = inputs[0].type;
        float amount = inputs[0].amount;

        for (int i = 0; i < outputs.Length; i++) {
            NewShipComponentOutput output = outputs[i];
            output.Set(passType, _distribution[i] * amount);
        }

        string textualDistribution = "";
        foreach (float percentage in _distribution) {
            textualDistribution += (int)(percentage * 100) + "% - ";
        }
        SetStatus("Distributing " + amount + " " + passType + " by " +
            // Removing the extra ' - ' from the distribution text
            textualDistribution.Substring(0, textualDistribution.Length - 3));
        return outputs;
    }
}
}