using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interstalator {
public class SwitchParametersController : MonoBehaviour {

    public WaterSwitch switchComponent;
    private GameObject sliderBase;
    private RectTransform panelRect;
    private GameObject[] sliders;

    void Awake() {
        panelRect = GetComponent<RectTransform>();
        sliderBase = transform.Find("Slider").gameObject;
    }

    public void TestSwitch() {
        BringUpSlider(new float[] {1f, 0f, 4f, 2f, -4}, null);
    }

    public void BringUpSlider(float[] currentDistribution, WaterSwitch switchComponent) {
        gameObject.SetActive(true);
        this.switchComponent = switchComponent;
        int numberOfSliders = currentDistribution.Length;
        sliders = new GameObject[numberOfSliders];

        // Used to distribute the slider components in the center of the panel
        int sliderDistance = (int)panelRect.rect.width / (numberOfSliders + 1);
        Debug.Log("Slider distance: " + sliderDistance);

        for (int i = 0; i < numberOfSliders; i++) {
            GameObject slider = Instantiate(sliderBase, transform);
            slider.gameObject.SetActive(true);
            RectTransform sliderRect = slider.GetComponent<RectTransform>();
            Vector3 pos = sliderRect.localPosition;
            pos.x = sliderDistance * (i + 1);
            pos.x -= (int)panelRect.rect.width / 2;
            sliderRect.localPosition = pos;
        }
    }


    public void Cancel() {
        gameObject.SetActive(false);
    }
}
}