using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCamT : MonoBehaviour
{
    private bool _camAvailable = false;
    private WebCamTexture _backCamera;
    private Texture _defaultBackground;

    [SerializeField] private RawImage _background;
    [SerializeField] private AspectRatioFitter _fit;

    void Start()
    {
        _defaultBackground = _background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        for (int i = 0; i < devices.Length; i++)
        {
            if (!devices[i].isFrontFacing)
            {
                _backCamera = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
                _backCamera.Play();
                _background.texture = _backCamera;
                _camAvailable = true;
                break;
            }
        }
    }
    void Update()
    {
        float ratio = (float)_backCamera.width / _backCamera.height;
        _fit.aspectRatio = ratio;

        float scaleY = _backCamera.videoVerticallyMirrored ? -1f : 1f;
        _background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orient = -_backCamera.videoRotationAngle;
        _background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
    }
}