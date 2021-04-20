// using UnityEditor;
// using UnityEditor.SceneManagement;
// using UnityEngine;
//
//
// #if UNITY_EDITOR
// [CustomEditor(typeof(GameManager))]
// public class GameManager_Editor : Editor {
//     private GameManager gm;
//     
//     private void OnEnable() {
//         gm = (GameManager) target;
//     }
//
//     public override void OnInspectorGUI() {
//         // DrawDefaultInspector();
//         
//         if (gm) {
//             gm.audioManager = EditorGUILayout.ObjectField("Audio Manager", gm.audioManager, typeof(AudioManager), true) as AudioManager;
//             EditorGUILayout.Space();
//             gm.mainMenu = EditorGUILayout.ObjectField("Main Menu", gm.mainMenu, typeof(Canvas), true) as Canvas;
//             gm.settingsMenu = EditorGUILayout.ObjectField("Settings Menu", gm.settingsMenu, typeof(Canvas), true) as Canvas;
//             gm.pauseMenu = EditorGUILayout.ObjectField("Pause Menu", gm.pauseMenu, typeof(Canvas), true) as Canvas;
//             EditorGUILayout.Space();
//             gm.loadScreen = EditorGUILayout.ObjectField("Load Screen", gm.loadScreen, typeof(LoadScreen), true) as LoadScreen;
//             gm.hud = EditorGUILayout.ObjectField("HUD", gm.hud, typeof(HUD), true) as HUD;
//             EditorGUILayout.Space();
//             gm.playerController = EditorGUILayout.ObjectField("Player Controller", gm.playerController, typeof(PlayerController), true) as PlayerController;
//             gm.cursorController = EditorGUILayout.ObjectField("Cursor Controller", gm.cursorController, typeof(CursorController), true) as CursorController;
//         }
//
//         if (GUI.changed) SetObj(gm.gameObject);
//     }
//
//     public static void SetObj(GameObject go) {
//         EditorUtility.SetDirty(go);
//         EditorSceneManager.MarkSceneDirty(go.scene);
//     }
// }
// #endif
