using UnityEngine;


public struct Cursors {
    private static bool isInit;
    public static Cursors System, Dot, Hand, Grab;

    public string Name;
    public Texture2D Texture;

    public static void Init() {
        if (isInit) return;
        System.Name = "system";
        Dot.Name = "dot";
        Hand.Name = "hand";
        Grab.Name = "grab";

        System.Texture = null;
        Dot.Texture = Resources.Load(Res.Sprites.СursorDot, typeof(Texture2D)) as Texture2D;
        Hand.Texture = Resources.Load(Res.Sprites.СursorHand, typeof(Texture2D)) as Texture2D;
        Grab.Texture = Resources.Load(Res.Sprites.СursorGrab, typeof(Texture2D)) as Texture2D;
    }
}


public delegate void CursorChanged(Cursors cursor);
public class CursorController : MonoBehaviour {
    private Cursors[] cursors;
    private GameManager gameManager;
    private PlayerController playerController;
    private bool cursorVisible, cursorLocked;

    private void Awake() {
        Cursors.Init();
        cursors = new [] { Cursors.System, Cursors.Dot, Cursors.Hand, Cursors.Grab };
        
        gameManager = GameObject.FindGameObjectWithTag(Tags.GM.GameManager).GetComponent<GameManager>();
        playerController = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerController>();

        gameManager.CursorChanged += SetCursor;
        playerController.CursorChanged += SetCursor;
        SetCursor(Cursors.Dot);
    }

    private void Update() {
        if (gameManager.gamePaused) return;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    private void OnDisable() {
        gameManager.CursorChanged -= SetCursor;
        playerController.CursorChanged -= SetCursor;
        SetCursor(Cursors.System);
        UnlockCursor();
        MakeCursorVisible();
    }

    private void SetCursor(Cursors cursorToSet) {
        foreach (var cursor in cursors) 
            if (cursor.Name == cursorToSet.Name) 
                Cursor.SetCursor(cursorToSet.Texture, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void MakeCursorVisible() {
        Cursor.visible = true;
        cursorVisible = true;
    }

    public void MakeCursorInvisible() {
        Cursor.visible = false;
        cursorVisible = false;
    }
    
    public void LockCursor() {
        Cursor.lockState = CursorLockMode.Locked;
        cursorLocked = true;
    }
    
    public void UnlockCursor() {
        Cursor.lockState = CursorLockMode.None;
        cursorLocked = false;
    }
}