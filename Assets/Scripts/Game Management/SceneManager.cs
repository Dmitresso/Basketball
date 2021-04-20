using System;
using System.IO;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public static class SceneManager {
    private static AsyncOperation loadScene;
    private static GameManager loadSceneGM, gameSceneGM;
    private static Coroutine loadScreenLoadingCoroutine;

    
    static SceneManager() { }

    
    private static GameManager GetSceneGameManager(string sceneName) {
        var gameObjects = UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName).GetRootGameObjects();
        foreach (var go in gameObjects)
            if (go.CompareTag(Tags.GM.GameManager))
                return go.GetComponent<GameManager>();
        throw new NullReferenceException("There's no \"Game Manager\" object on \"" + sceneName + "\" scene." );
    }
    
    
    public static string GetSceneNameByIndex(int index) {
        return Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(index));
    }

    
    public static IEnumerator LoadScene(string sceneName) {
        Scenes.GameToLoad = sceneName;
        yield return new WaitForSeconds(0.1f);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(Scenes.Load);
        loadScene = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(Scenes.GameToLoad);
        loadScene.allowSceneActivation = false;
    }

    
    public static void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name == Scenes.Load) {
            loadSceneGM = GetSceneGameManager(Scenes.Load);
            loadScreenLoadingCoroutine = loadSceneGM.StartCoroutine(loadSceneGM.loadScreen.StartLoading());
            loadSceneGM.loadScreen.LoadingFinished += ActivateGameScene;
        }
        else {
            if (loadScreenLoadingCoroutine != null) GetSceneGameManager(scene.name).StopCoroutine(loadScreenLoadingCoroutine);
        }
    }
    
    
    private static void ActivateGameScene() {
        if (loadScene != null) {
            loadScene.allowSceneActivation = true;
        }
        else {
            string e = "loadscene is null. It's ok if \"LoadScene\" scene was launched in single mode.";
            throw new Exception(e);
        }
    }

    
    public static void ExitApp() {
        Application.Quit();
    }
}