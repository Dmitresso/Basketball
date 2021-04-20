using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour {
    [SerializeField] private float minX = -60f, maxX = 60f;
    [SerializeField] private float minY = -360f, maxY = 360f;
    [SerializeField] private float sensitivityX, sensitivityY;

    private Camera camera;
    private float rotX, rotY, torque;
    
    private float hInput, vInput;
    private const string Hk = "Horizontal", Vk = "Vertical";
    private const string Hm = "Mouse X", Vm = "Mouse Y";

    private void Awake() {
        camera = GetComponent<Camera>();
    }

    private void Update() {
        hInput = Input.GetAxis(Hm);
        vInput = Input.GetAxis(Vm);

        rotX += vInput * sensitivityX;
        rotY += hInput * sensitivityY;

        rotX = Mathf.Clamp(rotX, minX, maxX);
        rotY = Mathf.Clamp(rotY, minY, maxY);
        
        transform.localEulerAngles = new Vector3(0, rotY, 0);
        camera.transform.localEulerAngles = new Vector3(-rotX, rotY, 0);
    }
}
