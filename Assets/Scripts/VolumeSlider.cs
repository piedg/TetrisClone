using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] Slider Slider;

    private void Start()
    {
        Slider.onValueChanged.AddListener(val => AudioManager.Instance.ChangeMasterVolume(val));
    }
}
