using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFaderTest : MonoBehaviour
{
    [SerializeField] private CameraFader _fader;
    [SerializeField] private KeyCode _fadeToBlackKey;
    [SerializeField] private KeyCode _fadeToTransparentKey;

    private void Update()
    {
        if (Input.GetKeyDown(_fadeToBlackKey))
            _fader.FadeToBlack(1);
        if (Input.GetKeyDown(_fadeToTransparentKey))
            _fader.FadeToTransparent(1);
    }
}
