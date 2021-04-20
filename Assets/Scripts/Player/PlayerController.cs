using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;


public delegate void PickableObject(bool isAffected);

[RequireComponent(typeof(Camera),
                 typeof(PhysicsRaycaster),
                 typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
    public event CursorChanged CursorChanged;
    public event PickableObject ObjectHovered;


    [SerializeField] private AudioClip[] stepSounds;
    [SerializeField] private Transform slot;

    [SerializeField] private float throwForceMultiplier = 2f;
    [SerializeField] private float walkSpeed = 5.5f;
    [SerializeField] private float runSpeed = 8.5f;
    [SerializeField] private float jumpSpeed = 8f;
    [SerializeField] private float gravity = 20f;
    [SerializeField] private float lookSpeed = 2f;
    //[SerializeField] private float lookXLimit = 45f;


    private CharacterController characterController;
    private GameManager gameManager;
    private AudioManager audioManager;
    private Camera playerCamera;
    private ThrowBar throwBar;
    private PickableItem pickedItem;

    private bool initItem, hoverItem, holdItem;
    private bool isRunning, canMove = true;
    private float hitDistance = 3f;
    private float throwForce = 10f;
    private Vector3 moveDirection = Vector3.zero;


    // Inputs
    private Vector3 mousePosition;
    private float rotationX, rotationY;
    private float khInput, kvInput, mhInput, mvInput;
    private const string kH = "Horizontal", kV = "Vertical", mH = "Mouse X", mV = "Mouse Y";
    private const int leftMouseButton = 0, rightMouseButton = 1, middleMouseButton = 2;
    private bool lmbPressed, rmbPressed, mmbPressed;


    [SerializeField] private Keys keys = new Keys(
        KeyCode.W,
        KeyCode.S,
        KeyCode.A,
        KeyCode.D,
        KeyCode.Space,
        KeyCode.LeftShift
    );

    private void Awake() {
        characterController = GetComponent<CharacterController>();
        playerCamera = GetComponent<Camera>();
        
        throwBar = GameObject.FindGameObjectWithTag(Tags.ThrowBar).GetComponent<ThrowBar>();
        gameManager = GameObject.FindGameObjectWithTag(Tags.GM.GameManager).GetComponent<GameManager>();
        audioManager = GameObject.FindGameObjectWithTag(Tags.GM.AudioManager).GetComponent<AudioManager>();
    }

    private void Start() {
        throwBar.gameObject.SetActive(false);
    }

    private void Update() {
        if (gameManager.gamePaused) return;

        khInput = Input.GetAxis(kH);
        kvInput = Input.GetAxis(kV);
        mhInput = Input.GetAxis(mH);
        mvInput = Input.GetAxis(mV);
        mousePosition = Input.mousePosition;
        lmbPressed = Input.GetMouseButton(leftMouseButton);


        var velocity = characterController.velocity;
        bool isMoving = velocity.x != 0 || velocity.z != 0;

        Vector3 x = transform.TransformDirection(Vector3.right);
        Vector3 z = transform.TransformDirection(Vector3.forward);
        isRunning = Input.GetKey(keys.run);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * khInput : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * kvInput : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = x * curSpeedX + z * curSpeedY;

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded) moveDirection.y = jumpSpeed;
        else moveDirection.y = movementDirectionY;
        
        if (!characterController.isGrounded) moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);
        if (isMoving && characterController.isGrounded && !audioManager.IsPlaying(AudioSrc.PlayerSound)) audioManager.Play(stepSounds[Random.Range(0, stepSounds.Length)], AudioSrc.PlayerSound);
        
        if (!canMove) return;
        rotationX += -mvInput * lookSpeed;
        rotationY += mhInput * lookSpeed;
        
        //rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
        transform.rotation *= Quaternion.Euler(mvInput * lookSpeed, mhInput * lookSpeed, 0);


        if (hoverItem && lmbPressed) {
            holdItem = true;
        }
        else {
            if (holdItem) DropItem(pickedItem);
            holdItem = false;
            throwBar.gameObject.SetActive(false);
        }

        if (holdItem) {
            throwBar.gameObject.SetActive(true);
            PickItem(pickedItem);
            CursorChanged?.Invoke(Cursors.Grab);
            ObjectHovered?.Invoke(false);
        }
    }

    private void FixedUpdate() {
        if (!holdItem) {
            RaycastHit hit;
            var ray = playerCamera.ViewportPointToRay(Vector3.one * 0.5f);
            if (Physics.Raycast(ray, out hit, hitDistance) && hit.transform.CompareTag(Tags.Items.Ball)) {
                //Debug.DrawRay(playerCamera.transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                //Debug.Log("Did Hit");

                hoverItem = true;
                pickedItem = hit.transform.GetComponent<PickableItem>();

                ObjectHovered?.Invoke(true);
                if (holdItem) return;
                CursorChanged?.Invoke(Cursors.Hand);
            }
            else {
                //Debug.DrawRay(playerCamera.transform.position, transform.TransformDirection(Vector3.forward) * hitDistance, Color.white);
                //Debug.Log("Did not Hit");
                hoverItem = false;
                holdItem = false;           
                CursorChanged?.Invoke(Cursors.Dot);
                ObjectHovered?.Invoke(false);
            }
        }
    }

    
    private void PickItem(PickableItem item) {
        pickedItem = item;
        //item.Rb.isKinematic = true;
        item.Rb.velocity = Vector3.zero;
        item.Rb.angularVelocity = Vector3.zero;
        item.transform.SetParent(slot);
        item.transform.localPosition = Vector3.zero;
        item.transform.localEulerAngles = Vector3.zero;
    }
    
    private void DropItem(PickableItem item) {
        pickedItem = null;
        var t = item.transform;
        var f = t.forward * throwForceMultiplier * throwBar.currentValue;
        t.SetParent(null);
        //item.Rb.isKinematic = false;
        item.Rb.AddForce(f, ForceMode.VelocityChange);
    }
}