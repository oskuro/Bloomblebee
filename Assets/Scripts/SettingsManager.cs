using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] float minMouseSens = 15f;
    [SerializeField] float maxMouseSens = 90f;
    public Toggle toggleInvertHorizontal;
    public Toggle toggleInvertVertical;

    public Slider horizontalSlider;
    public Slider verticalSlider;

    public Slider volumeSlider;
    public AudioMixer audioMixer;

    CameraLook cameraLook;


    void Start()
    {
        cameraLook = FindObjectOfType<CameraLook>();

        RestoreValues();
        SetupUI();
        
    }

    private void RestoreValues()
    {
        cameraLook.horizontalTurningSpeed = GameManager.instance.HorizontalSensitivity;
        cameraLook.verticalTurningSpeed = GameManager.instance.VerticalSensitivity;

        if (GameManager.instance.InvertHorizontal)
            cameraLook.lookInvert.y = -1;
        else
            cameraLook.lookInvert.y = 1;

        if (GameManager.instance.InvertVertical)
            cameraLook.lookInvert.x = -1;
        else
            cameraLook.lookInvert.x = 1f;

        audioMixer.SetFloat("volume", GameManager.instance.Volume);

    }

    public void SetHorizontalMouseSensitivity(float value)
    {
        float newValue = Mathf.Lerp(minMouseSens, maxMouseSens, value);
        cameraLook.horizontalTurningSpeed = newValue;
        GameManager.instance.HorizontalSensitivity = newValue;
    }

    public void SetVerticalMouseSensitivity(float value)
    {
        float newValue = Mathf.Lerp(minMouseSens, maxMouseSens, value);
        cameraLook.verticalTurningSpeed = newValue;
        GameManager.instance.VerticalSensitivity = newValue;

    }

    public void SetVolume(float _volume)
    {
        if (!audioMixer)
            return;

        audioMixer.SetFloat("volume", _volume);
        GameManager.instance.Volume = _volume;
    }

    public void InvertMouseLookHorizontal(bool _invert)
    {
        if(_invert)
            cameraLook.lookInvert.y = -1;
        else
            cameraLook.lookInvert.y = 1;

        GameManager.instance.InvertHorizontal = _invert;
    }

    public void InvertMouseLookVertical(bool _invert)
    {
        if(_invert)
            cameraLook.lookInvert.x = -1;
        else
            cameraLook.lookInvert.x = 1;

        GameManager.instance.InvertVertical = _invert;
    }

    void SetupUI()
    {
        if (GameManager.instance.InvertVertical)
        {
            toggleInvertVertical.isOn = true;
        }
        else
        {
            toggleInvertVertical.isOn = false;
        }

        if (GameManager.instance.InvertHorizontal)
        {
            toggleInvertHorizontal.isOn = false;
        }
        else
        {
            toggleInvertHorizontal.isOn = true;
        }

        horizontalSlider.value = GameManager.instance.HorizontalSensitivity / (maxMouseSens - minMouseSens);
        verticalSlider.value = GameManager.instance.VerticalSensitivity / (maxMouseSens - minMouseSens);

        volumeSlider.value = GameManager.instance.Volume;
    }
    
}
