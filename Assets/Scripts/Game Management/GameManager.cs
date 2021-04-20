using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;


[Serializable]
public class GameManager : Singleton<MonoBehaviour> {
    public event CursorChanged CursorChanged;

    private bool pausable;
    public bool gamePaused;
    public bool gameOver;
    

    [SerializeField] public AudioManager audioManager;
    [SerializeField] public ResolutionManager resolutionManager;
    [SerializeField] public Canvas mainMenu;
    [SerializeField] public Canvas settingsMenu;
    [SerializeField] public Canvas difficultyMenu;
    [SerializeField] public Canvas pauseMenu;
    [SerializeField] public Canvas gameoverMenu;
    [SerializeField] public LoadScreen loadScreen;
    [SerializeField] public HUD hud;
    [SerializeField] public PlayerController playerController;
    [SerializeField] public CursorController cursorController;
    
    private TextMeshProUGUI gameoverText;
    
    private Timer timer;
    private Score score;
    private BallsPool ballsPool;
    [HideInInspector] public List<string> dropdownMenuOptions = new List<string>();

    
    
    private void Awake() {
        Init(gameObject.scene.name);
    }

    private void OnEnable() {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += SceneManager.OnSceneLoaded;
    }

    private void OnDisable() {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= SceneManager.OnSceneLoaded;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) PressPause();
    }

    private void Init(string scene) {
        DATA.INIT();
        if (scene == Scenes.Main) {
            StartCoroutine(InitResolutions());
            difficultyMenu.gameObject.SetActive(false);
        }
        else if (scene == Scenes.Hoops) {
            pausable = true;
            
            gameoverText = GameObject.FindGameObjectWithTag(Tags.GameoverText).GetComponent<TextMeshProUGUI>();
            ballsPool = GameObject.FindGameObjectWithTag(Tags.BallsPool).GetComponent<BallsPool>();
            timer = hud.GetComponentInChildren<Timer>();
            score = hud.GetComponentInChildren<Score>();

            if (!Settings.DifficultyLevel.selectedFromMain) Settings.DifficultyLevel.Selected = Settings.DifficultyLevel.Test;
            timer.TimeLeft = Settings.DifficultyLevel.Selected.TimeLeft;
            score.TargetScore = Settings.DifficultyLevel.Selected.TargetScore;
            ballsPool.BallsMax = Settings.DifficultyLevel.Selected.BallsMax;
            
            pauseMenu.gameObject.SetActive(false);
            gameoverMenu.gameObject.SetActive(false);
        }
    }
    
    private IEnumerator InitResolutions() {
        yield return new WaitUntil(() => resolutionManager.IsInit);
        foreach (var r in resolutionManager.windowedResolutions) dropdownMenuOptions.Add(r.x + "×" + r.y);
        
        if (resolutionManager.fullscreenResolutions.Count >= 2) {
            dropdownMenuOptions.Add(resolutionManager.fullscreenResolutions[resolutionManager.fullscreenResolutions.Count - 2].x + "×" + resolutionManager.fullscreenResolutions[resolutionManager.fullscreenResolutions.Count - 2].y + " (Fullscreen)");
        }
        dropdownMenuOptions.Add(resolutionManager.fullscreenResolutions[resolutionManager.fullscreenResolutions.Count - 1].x + "×" + resolutionManager.fullscreenResolutions[resolutionManager.fullscreenResolutions.Count - 1].y + " (Fullscreen)");
    }
    
    private void PauseGame() {
        gamePaused = true;
        Time.timeScale = 0;
        audioManager.SetVolume(0.3f);
        pauseMenu.gameObject.SetActive(true);
        cursorController.UnlockCursor();
        cursorController.MakeCursorVisible();
        CursorChanged?.Invoke(Cursors.System);
    }
    
    private void ResumeGame() {
        gamePaused = false;
        Time.timeScale = 1;
        audioManager.SetVolume(0.8f);
        pauseMenu.gameObject.SetActive(false);
        cursorController.LockCursor();
        cursorController.MakeCursorInvisible();
        CursorChanged?.Invoke(Cursors.Dot);
    }
    
    public void PressPause() {
        if (!pausable) return;
        if (gamePaused) ResumeGame();
        else PauseGame();
    }
    
    public void GameOver() {
        timer.StopTimer();
        
        var isWin = false;
        gameOver = true;
        pausable = false;

        if (score.scorePoints == score.TargetScore) isWin = true;
        var s = "Your score: " + score.scorePoints + "/" + score.TargetScore;
        var t = "Your time: " + timer.playTime;
        if (isWin) gameoverText.text = "Congratulation!\nYou won!\n" + s + "\n" + t;
        else gameoverText.text = "You lose..." + "\n" + s;
        
        hud.gameObject.SetActive(false);
        gameoverMenu.gameObject.SetActive(true);
        CursorChanged?.Invoke(Cursors.System);
        cursorController.enabled = false;
        playerController.enabled = false;
        
    }
    
    public void StartGame(string gameName) {
        StartCoroutine(SceneManager.LoadScene(gameName));
    }
    
    public void RestartGame() {
        StartCoroutine(SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name));
    }

    public void ExitGame() {
        StartCoroutine(SceneManager.LoadScene(Scenes.Main));
    }

    public void ExitApp() {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #endif        
        
        SceneManager.ExitApp();
    }
}