using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleMovementManager : MonoBehaviour
{
    public Transform cube;
    private bool CanRotate = false;
    float value;
    // Start is called before the first frame update
    void Start()
    {
        CanRotate = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(CanRotate)
        cube.Rotate(Vector3.forward * (1+(value*10)));
    }

    public void toggleRotate()
    {
        if (CanRotate)
            CanRotate = false;
        else
            CanRotate = true;
    }

    public void AdjustSpeed(Object obj)
    {
        SliderValue slide = new SliderValue();
        slide = obj as SliderValue;
        value = slide.value;
    }
}
