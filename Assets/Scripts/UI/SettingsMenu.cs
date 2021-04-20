﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {
    public event VolumeChange SoundtrackVolumeChanged;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] public TMP_Dropdown dropdownMenu;
    private ResolutionManager resolutionManager;
    private GameManager gameManager;

    private void Awake() {
        gameManager = GameObject.FindGameObjectWithTag(Tags.GM.GameManager).GetComponent<GameManager>();
        resolutionManager = GameObject.FindGameObjectWithTag(Tags.GM.ResolutionManager).GetComponent<ResolutionManager>();
        volumeSlider.value = 0.5f;
        volumeSlider.onValueChanged.AddListener(VolumeSliderValueChanged);
        dropdownMenu.onValueChanged.AddListener(DropdownValueChanged);
        dropdownMenu.ClearOptions();
        gameObject.SetActive(false);
    }

    private void Start() {
        dropdownMenu.AddOptions(gameManager.dropdownMenuOptions);
        dropdownMenu.value = gameManager.dropdownMenuOptions.Count - 1;
    }


    private void DropdownValueChanged(int change) {
        bool fullscreen = change > resolutionManager.windowedResolutions.Count - 1;
        resolutionManager.SetResolution(change, fullscreen);
    }

    private void VolumeSliderValueChanged(float value) {
        Settings.UserVolume = value;
        volumeSlider.value = value;
        SoundtrackVolumeChanged?.Invoke(value);
    }
}