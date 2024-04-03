using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SampleUIManager : MonoBehaviour
{

    public Button StopButton;
    public Slider sliderBar;
    public Text StopButtonText;
    public GameEvent StopEvent;
    public GameEvent sliderEvent;
    bool isStopped = false;
    private void OnEnable()
    {
        StopButton.onClick.AddListener(StopButtonAction);
        sliderBar.onValueChanged.AddListener(SliderValue);
    }

    private void OnDisable()
    {
        StopButton.onClick.RemoveListener(StopButtonAction);
        sliderBar.onValueChanged.AddListener(SliderValue);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StopButtonAction()
    {
        if(isStopped)
        {
            isStopped = false;
            StopButtonText.text = "Stop";
        }
        else
        {
            isStopped = true;
            StopButtonText.text = "Start";

        }
        StopEvent?.InvokeEvent();
    }

    void SliderValue(float val)
    {
        Object obj = new Object();
        SliderValue slide = new SliderValue();
        slide.value = val;
        obj = slide;
        sliderEvent?.InvokeEvent(obj);
    }
}

public class SliderValue : Object
{
    public float value;
}

