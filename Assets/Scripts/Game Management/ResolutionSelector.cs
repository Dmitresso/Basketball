using UnityEngine;

public class ResolutionSelector : MonoBehaviour  {
    void OnGUI() {
        if (ResolutionManager.Instance == null) return;

        ResolutionManager resolutionManager = ResolutionManager.resolutionManager;

        GUILayout.BeginArea(new Rect(20, 10, 200, Screen.height - 10));

        GUILayout.Label("Select Resolution");

        if (GUILayout.Button(Screen.fullScreen ? "Windowed" : "Fullscreen")) resolutionManager.ToggleFullscreen();

        int i = 0;
        foreach (Vector2 r in Screen.fullScreen ? resolutionManager.fullscreenResolutions : resolutionManager.windowedResolutions) {
            string label = r.x + "×" + r.y;
            if (r.x == Screen.width && r.y == Screen.height) label += "*";
            if (r.x == resolutionManager.DisplayResolution.width && r.y == resolutionManager.DisplayResolution.height) label += " (native)";

            if (GUILayout.Button(label))
                resolutionManager.SetResolution(i, Screen.fullScreen);

            i++;
        }

        if (GUILayout.Button("Get Current Resolution")) {
            Resolution r = Screen.currentResolution;
            Debug.Log("Display resolution is " + r.width + "×" + r.height);
        }

        GUILayout.EndArea();
    }
}