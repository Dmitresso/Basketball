using UnityEngine;
using Random = UnityEngine.Random;


public class Ball : MonoBehaviour {
    [SerializeField] private AudioClip ballBounceSound;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material highlightedMaterial;
    [SerializeField] private Texture[] textures;

    private AudioManager audioManager;    
    private Texture ballTexture;
    private Renderer meshRenderer;
    private PlayerController playerController;
    private BallsPool ballsPool;
    
    private bool isGrounded;
    private float clickableDistance = 2f;
    
    
    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag(Tags.GM.AudioManager).GetComponent<AudioManager>();
        ballTexture = textures[Random.Range(0, textures.Length)];
        defaultMaterial.mainTexture = ballTexture;
        highlightedMaterial.mainTexture = ballTexture;
        
        meshRenderer = GetComponent<MeshRenderer>();
        ballsPool = GameObject.FindGameObjectWithTag(Tags.BallsPool).GetComponent<BallsPool>();
        playerController = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerController>();
        playerController.ObjectHovered += Highlight;
    }
    

    private void Update() {
        if (transform.position.y < 0) {
            ballsPool.RemoveBall(this);
        }
    }

    private void OnDisable() {
        playerController.ObjectHovered -= Highlight;
    }
    
    
    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.CompareTag(Tags.Env.Ground) ||
            collision.transform.CompareTag(Tags.Env.Wall) ||
            collision.transform.CompareTag(Tags.Items.Basket)) {
            audioManager.Play(ballBounceSound, AudioSrc.BallSound);
        }
    }
    

    private void Highlight(bool isHovered) {
        meshRenderer.material = isHovered ? highlightedMaterial : defaultMaterial;
    }
}
