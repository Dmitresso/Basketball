using UnityEngine;


public class Basket : MonoBehaviour {
    
    [HideInInspector] public bool first;
    [HideInInspector] public bool second;
    [SerializeField] private AudioClip goalSound;

    private AudioManager audioManager;
    private Score score;

    
    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag(Tags.GM.AudioManager).GetComponent<AudioManager>();
        score = GameObject.FindGameObjectWithTag(Tags.GM.HUD).GetComponentInChildren<Score>();
    }

    public void Goal() {
        first = false;
        second = false;
        score.ScoreUp(1);
        audioManager.Play(goalSound);
    }
}
