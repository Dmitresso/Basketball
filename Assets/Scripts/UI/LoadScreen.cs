using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public delegate void LoadingFinished();
    
public class LoadScreen : MonoBehaviour {
    public event LoadingFinished LoadingFinished;
    
    [SerializeField, Range(0.5f, 4f)] private float fadeDuration = 1.5f;
    [SerializeField] private Image fadeScreen;
    [SerializeField] public ProgressBar progressBar;
    [SerializeField] private TextMeshProUGUI loadingText;


    public IEnumerator StartLoading() {
        FadeIn();
        StartCoroutine(progressBar.StartLoading());
        StartCoroutine(FadeOut());
        StartCoroutine(SetReadyToUnload());
        yield return null;
    }

    private void FadeIn() {
        fadeScreen.CrossFadeAlpha(0, fadeDuration, false);
    }
    
    private IEnumerator FadeOut() {
        yield return new WaitUntil(progressBar.LoadingCompleted);
        fadeScreen.CrossFadeAlpha(1, fadeDuration, false);
    }

    private IEnumerator SetReadyToUnload() {
        yield return new WaitUntil(IsFadedOut);
        OnLoadingFinished();
    }

    public bool IsFadedIn() {
        return fadeScreen.canvasRenderer.GetAlpha() == 0f;
    }
    
    public bool IsFadedOut() {
        return fadeScreen.canvasRenderer.GetAlpha() == 1f;
    }

    protected virtual void OnLoadingFinished() {
        LoadingFinished?.Invoke();
    }
}