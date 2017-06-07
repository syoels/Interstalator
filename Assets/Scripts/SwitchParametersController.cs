using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Interstalator {

public abstract class SwitchCaller {
    public SwitchParametersController switchController;

    public abstract void ApplyDistribution(float[] newDistribution);
}

public class SwitchParametersController : MonoBehaviour {

    // Used to mark this as a singleton
    public static SwitchParametersController instance;

    // Remain static during run
    private GameObject sliderBase;
    private RectTransform panelRect;

    // Changes for each component that activates it
    private GenericSwitch switchComponent;
    private GameObject[] sliders;
    private float[] originalDistribution;
    private bool keepConstantAmount;
    private float totalAmount;
    private Slider lastChangedSlider;

    void Awake() {
        panelRect = GetComponent<RectTransform>();
        sliderBase = transform.Find("Slider").gameObject;
        // TODO: Make this work even though gmae object is inactive
        instance = this;
    }

    // TODO: Remove this
    public void TestSwitch() {
        BringUpSlider(new float[] {0.25f, 0.3f, 0.45f, 0f}, null, false);
    }

    /// <summary>
    /// Creates and brings up a new slider UI with the proper number of switches and the
    /// current values.
    /// </summary>
    /// <param name="currentDistribution">Current distribution.</param>
    /// <param name="switchComponent">Switch component - used to apply the effect on later</param>
    /// <param name="constantAmount">Keep the total sliders amount constant</param>
    public void BringUpSlider(float[] currentDistribution,
                            GenericSwitch switchComponent,
                            bool constantAmount=false) {
        // Show the switch UI
        gameObject.SetActive(true);

        this.switchComponent = switchComponent;
        this.originalDistribution = currentDistribution;
        this.keepConstantAmount = constantAmount;
        totalAmount = 0;

        int numberOfSliders = currentDistribution.Length;
        sliders = new GameObject[numberOfSliders];

        // Used to distribute the slider components in the center of the panel
        int sliderDistance = (int)panelRect.rect.width / (numberOfSliders + 1);

        for (int i = 0; i < numberOfSliders; i++) {
            // Calculate the amount of all the values
            totalAmount += currentDistribution[i];

            // Places a new slider and sets its attributes
            GameObject slider = Instantiate(sliderBase, transform);
            slider.gameObject.SetActive(true);
            RectTransform sliderRect = slider.GetComponent<RectTransform>();

            Vector3 pos = sliderRect.localPosition;
            pos.x = sliderDistance * (i + 1);
            // For some reason the slider's anchor is in the middle of the panel instead
            // of its left side. We use this to correct their position
            pos.x -= (int)panelRect.rect.width / 2;
            sliderRect.localPosition = pos;

            // Set the slider's value (will only properly show values from 0f to 1f)
            Slider sliderScript = slider.GetComponent<Slider>();
            sliderScript.value = currentDistribution[i];
            if (constantAmount) {
                sliderScript.onValueChanged.AddListener(delegate {OnSliderValueChanged(sliderScript);});
            }
            sliders[i] = slider;
        }
    }

    // Rememebers the last slider that was changed. Later used in Update to change the rest of them
    private void OnSliderValueChanged(Slider slider) {
        lastChangedSlider = slider;
    }

    /// <summary>
    /// Changes the unused slider's amount to keep the level equal
    /// </summary>
    /// <param name="sliderScript">Slider script - the slider script that changed</param>
    private void KeepSlidersAmount(Slider sliderScript) {
        // TODO - Find a way to make this more efficient and avoid cycleing through
        // all the sliders twice
        float currentAmount = 0;
        foreach (GameObject slider in sliders) {
            currentAmount += slider.GetComponent<Slider>().value;
        }

        float changeAmount = (totalAmount - currentAmount) / (sliders.Length - 1);
        foreach (GameObject slider in sliders) {
            Slider sScript = slider.GetComponent<Slider>();

            if (sScript != sliderScript) {
                sScript.value += changeAmount;
            }
        }
        lastChangedSlider = null;
    }

    void Update() {
        // Change the sliders' value according to the original full amount
        if (keepConstantAmount && lastChangedSlider != null) {
            KeepSlidersAmount(lastChangedSlider);
        }
    }

    private float[] GetCurrentDistribution() {
        float[] distribution = new float[sliders.Length];

        for (int i = 0; i < sliders.Length; i++) {
            Slider sliderScript = sliders[i].GetComponent<Slider>();
            distribution[i] = sliderScript.value;
        }

        return distribution;
    }

    /// <summary>
    /// Removes references for switch controls, deletes the created sliders
    /// and hides the UI
    /// </summary>
    private void ClearSwitchControls() {
        switchComponent = null;
        originalDistribution = null;
        keepConstantAmount = false;
        lastChangedSlider = null;
        totalAmount = 0;

        foreach (GameObject slider in sliders) {
            Destroy(slider);
        }

        sliders = null;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Returns the slider values to their original state
    /// </summary>
    public void Reset() {
        for (int i = 0; i < sliders.Length; i++) {
            Slider sliderScript = sliders[i].GetComponent<Slider>();
            sliderScript.value = originalDistribution[i];
        }
    }

    /// <summary>
    /// Turns off the sliders UI, deletes the created sliders and resets everything
    /// </summary>
    public void Cancel() {
        ClearSwitchControls();
    }


    /// <summary>
    /// Saves the new distribution in the calling switch component
    /// </summary>
    public void Apply() {
        // TODO: Need to remove this once we stopped using tests
        if (switchComponent == null) {
            Debug.LogError("Tried applying switch controls without switch component");
        } else {
            switchComponent.ApplyDistribution(GetCurrentDistribution());
        }
        ClearSwitchControls();
    }


}
}