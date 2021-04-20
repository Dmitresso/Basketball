using UnityEngine;


public class Preferences : MonoBehaviour {
    public static void SetOption(string option, string value) {
        PlayerPrefs.SetString(option, value);
    }

    public static void SetOption(string option, int value) {
        PlayerPrefs.SetInt(option, value);
    }    
    
    public static void SetOption(string option, float value) {
        PlayerPrefs.SetFloat(option, value);
    }    
    
    public static string GetOption(string option) {
        return PlayerPrefs.GetString(option);
    }
}
