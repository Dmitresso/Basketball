using UnityEngine;
using UnityEngine.EventSystems;


[RequireComponent(typeof(Collider2D))]
public class ActionButton : MonoBehaviour, IPointerEnterHandler {
    [SerializeField] private AudioClip buttonEnterSound;
    [SerializeField] private AudioClip buttonClickSound;
    
    private GameManager gameManager;
    private AudioManager audioManager;
    private Canvas difficultyMenu;
    private Canvas settingsMenu;
    private Canvas mainMenu;
    

    private void Start() {
        gameManager = GameObject.FindGameObjectWithTag(Tags.GM.GameManager).GetComponent<GameManager>();
        audioManager = GameObject.FindGameObjectWithTag(Tags.GM.AudioManager).GetComponent<AudioManager>();

        difficultyMenu = gameManager.difficultyMenu;
        settingsMenu = gameManager.settingsMenu;
        mainMenu = gameManager.mainMenu;
    }
    

    public void OnPointerEnter(PointerEventData eventData) {
        audioManager.Play(buttonEnterSound);
    }
    
    public void PlaySoundOnClick() {
        audioManager.Play(buttonClickSound);
    }
    
    public void PressPause() {
        gameManager.PressPause();
    }

    public void GoToDifficulty() {
        mainMenu.gameObject.SetActive(false);
        difficultyMenu.gameObject.SetActive(true);
    }
    
    public void GoToSettings() {
        mainMenu.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(true);
    }

    public void GoToMain() {
        difficultyMenu.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }

    public void SetDifficulty(string difficulty) {
        Settings.DifficultyLevel.selectedFromMain = true;
        Settings.DifficultyLevel.Selected = difficulty switch {
            "Easy" => Settings.DifficultyLevel.Easy,
            "Medium" => Settings.DifficultyLevel.Medium,
            "Hard" => Settings.DifficultyLevel.Hard,
            _ => Settings.DifficultyLevel.Easy
        };
    }
    
    public void StartGame(string gameName) {
        gameManager.StartGame(gameName);
    }
    
    public void RestartGame() {
        gameManager.RestartGame();
    }    
    
    public void ExitGame() {
        gameManager.ExitGame();
    }
    
    public void ExitApp() {
        gameManager.ExitApp();
    }
}