using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SampleColorManager : MonoBehaviour
{
    public Image indicator;

    private void OnEnable()
    {
        indicator.color = Color.green;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleColor()
    {
        if (indicator.color == Color.green)
            indicator.color = Color.red;
        else
            indicator.color = Color.green;
    }
}
