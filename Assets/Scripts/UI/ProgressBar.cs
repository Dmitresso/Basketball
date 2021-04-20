using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class ProgressBar : MonoBehaviour {
    [SerializeField, Range(1f, 5f)] public float duration = 2f;
    [SerializeField, Range(0.1f, 2f)] public float rate = 0.5f;
    [SerializeField] private AudioClip loadingCompleteSound;

    private AudioManager audioManager;
    private LoadScreen loadScreen;
    private Slider slider;
    

    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag(Tags.GM.AudioManager).GetComponent<AudioManager>();
        loadScreen = GetComponentInParent<LoadScreen>();
        slider = GetComponent<Slider>();
    }

    public IEnumerator StartLoading() {
        yield return new WaitUntil(loadScreen.IsFadedIn);
        while (!LoadingCompleted()) {
            var i = (slider.maxValue - slider.value) * rate / duration;
            slider.value += i;
            duration -= rate;
            yield return new WaitForSeconds(rate);
        }
        audioManager.Play(loadingCompleteSound, 0.5f);
    }

    public bool LoadingCompleted() {
        return slider.value >= slider.maxValue;
    }
}