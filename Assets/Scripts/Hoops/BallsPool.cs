using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallsPool : MonoBehaviour {
    [SerializeField] private GameObject ballPrefab;
    [SerializeField, Range(1, poolSize)] private int ballsMax = 3;
    private const int poolSize = 10;

    private List<GameObject> balls;
    private int ballsNow;
    private float xBound, zBound;
    private float xResp, yResp, zResp;

    public int BallsMax {
        get => ballsMax;
        set => ballsMax = value;
    }


    private void Awake() {
        balls = new List<GameObject>(poolSize);
        
        var bounds = GameObject.FindGameObjectWithTag(Tags.Env.Ground).GetComponent<Renderer>().bounds.extents;
        xBound = bounds.x;
        zBound = bounds.z;

        for (int i = 0; i < balls.Capacity; i++) {
            var ball = Instantiate(ballPrefab, RandomOnPlayground(), Quaternion.identity);
            ball.SetActive(false);
            balls.Add(ball);
        }
    }

    private void Start() {
        while (ballsNow != ballsMax) {
            SpawnRandomBall();
        }
    }

    public void RemoveBall(Ball ball) {
        ball.gameObject.SetActive(false);
        ball.transform.position = RandomOnPlayground();
        
        ballsNow--;
        if (ballsNow < ballsMax) SpawnRandomBall();
    }

    private void SpawnRandomBall() {
        balls[Random.Range(0, balls.Capacity)].SetActive(true);
        ballsNow++;
    }
    
    private Vector3 RandomOnPlayground() {
        xResp = Random.Range(-xBound + 2f, xBound - 2f);
        yResp = Random.Range(1f, 4f);
        zResp = Random.Range(-zBound + 2f, zBound - 2f);
        return new Vector3(xResp, yResp, zResp);
    }
}

// (Random.Range(0, 2) * 2 - 1)