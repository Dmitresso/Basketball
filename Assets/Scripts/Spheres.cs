using UnityEngine;
using Random = UnityEngine.Random;

public class Spheres : MonoBehaviour {
    private void Awake() {
        transform.rotation = new Quaternion(Random.Range(0f, 90f), Random.Range(0f, 90f), Random.Range(0f, 90f), 0f);
    }
}