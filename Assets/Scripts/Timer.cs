using System.Collections;
using TMPro;
using UnityEngine;
 

[RequireComponent(typeof(TextMeshProUGUI))]
public class Timer : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private float insaneTickSoundStart = 5f;
    [SerializeField] private float timeLeft = 300.0f;
    [SerializeField] private bool stop = true;

    [SerializeField] private AudioClip tickSound;
    [SerializeField] private AudioClip insaneTickSound;
    
    [HideInInspector] public string playTime;
    public float startTime, endTime;
    
    private GameManager gameManager;
    
    public float TimeLeft {
        get => timeLeft;
        set => timeLeft = value;
    }

    private AudioManager audioManager;
    
    private float minutes;
    private float seconds;
    private bool playSound = true;
    

    private void Awake() {
        gameManager = GameObject.FindGameObjectWithTag(Tags.GM.GameManager).GetComponent<GameManager>();
        audioManager = GameObject.FindGameObjectWithTag(Tags.GM.AudioManager).GetComponent<AudioManager>();
        label = GetComponent<TextMeshProUGUI>();
    }

    private void Start() {
        startTime = timeLeft;
        StartTimer(timeLeft);
    }

    private void Update() {
        if (stop) return;
        timeLeft -= Time.deltaTime;
        
        endTime = timeLeft;
        if (playSound && timeLeft <= insaneTickSoundStart) {
            label.color = Color.red;
            audioManager.Play(insaneTickSound);
            playSound = false;
        }

        minutes = Mathf.Floor(timeLeft / 60);
        seconds = timeLeft % 60;

        if (seconds > 59) seconds = 59;
        if (minutes < 0) {
            minutes = 0;
            seconds = 0;
            gameManager.GameOver();
        }
        // fraction = (timeLeft * 100) % 100;
    }
    
    
    public void StartTimer(float from) {
        stop = false;
        timeLeft = from;
        StartCoroutine(UpdateCoroutine());
    }
    
    public void StopTimer() {
        StopAllCoroutines();        
        stop = true;
        audioManager.Stop();
        var winTime = startTime - endTime;
        var min = .0; var sec = .0;
        if (winTime <= 60f) sec = winTime;
        else {
            min = Mathf.Floor(winTime / 60);
            sec = winTime % 60;
        }
        playTime = $"{min:00}:{sec:00.00}";
    }
        
    private IEnumerator UpdateCoroutine() {
        while (!stop) {
            label.text = $"{minutes:0}:{seconds:00}";
            yield return new WaitForSeconds(0.2f);
        }
    }
}