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

    // Remain static during run
    private GameObject sliderBase;
    private RectTransform panelRect;

    // Changes for each component that activates it
    private SwitchCaller switchComponent;
    private GameObject[] sliders;
    private float[] originalDistribution;
    private bool constantAmount;
    private float totalAmount;
    private Slider lastChangedSlider;

    void Awake() {
        panelRect = GetComponent<RectTransform>();
        sliderBase = transform.Find("Slider").gameObject;
    }

    public void TestSwitch() {
        BringUpSlider(new float[] {0.25f, 0.3f, 0.45f, 0f}, null, true);
    }

    /// <summary>
    /// Creates and brings up a new slider UI with the proper number of switches and the
    /// current values.
    /// </summary>
    /// <param name="currentDistribution">Current distribution.</param>
    /// <param name="switchComponent">Switch component - used to apply the effect on later</param>
    public void BringUpSlider(float[] currentDistribution,
                              SwitchCaller switchComponent,
                              bool constantAmount=false) {
        // Show the switch UI
        gameObject.SetActive(true);

        this.switchComponent = switchComponent;
        this.originalDistribution = currentDistribution;
        this.constantAmount = constantAmount;
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

        Debug.Log("Current amount: " + currentAmount);
        Debug.Log("Constant amount: " + totalAmount);
        float changeAmount = (totalAmount - currentAmount) / (sliders.Length - 1);
        Debug.Log("Cahnge amount: " + changeAmount);
        foreach (GameObject slider in sliders) {
            Slider sScript = slider.GetComponent<Slider>();

            if (sScript != sliderScript) {
                Debug.Log("Changing slider with: " + sScript.value);
                Debug.Log("To: " + (sScript.value + changeAmount));
                sScript.value += changeAmount;
            }
        }
        lastChangedSlider = sliderScript;
    }

    void Update() {
        if (constantAmount && lastChangedSlider != null) {
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