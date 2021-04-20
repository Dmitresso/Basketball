using TMPro;
using UnityEngine;


[RequireComponent(typeof(TextMeshProUGUI))]
public class Score : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI label;

    private string scoreText;
    public int scorePoints;
    private int targetScore;
    private GameManager gameManager;

    public int TargetScore {
        get => targetScore;
        set => targetScore = value;
    }

    private void Awake() {
        gameManager = GameObject.FindGameObjectWithTag(Tags.GM.GameManager).GetComponent<GameManager>();
        scoreText = "Score: 0/" + Settings.DifficultyLevel.Selected.TargetScore;
        label.text = scoreText;
    }

    public void ScoreUp(int points) {
        scorePoints += points;
        label.text = "Score: " + scorePoints + "/" + targetScore;
        if (scorePoints == targetScore) gameManager.GameOver();
    }
}
