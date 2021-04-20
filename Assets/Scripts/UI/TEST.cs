using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TEST : MonoBehaviour{
    public TMP_Dropdown dropdownMenu;
    public TMP_Dropdown menutoadd;
    private List<string> options = new List<string>();
    private ResolutionManager resolutionManager;
    
    private void Start() {
        var test = GameObject.FindGameObjectWithTag("TEST").GetComponent<TEST>();
        resolutionManager = GameObject.FindGameObjectWithTag(Tags.GM.ResolutionManager).GetComponent<ResolutionManager>();
        
        
        foreach (var r in resolutionManager.windowedResolutions) options.Add(r.x + "×" + r.y);
        
        if (resolutionManager.fullscreenResolutions.Count >= 2) {
            options.Add(resolutionManager.fullscreenResolutions[resolutionManager.fullscreenResolutions.Count - 2].x + "×" + resolutionManager.fullscreenResolutions[resolutionManager.fullscreenResolutions.Count - 2].y + " (Fullscreen)");
        }
        options.Add(resolutionManager.fullscreenResolutions[resolutionManager.fullscreenResolutions.Count - 1].x + "×" + resolutionManager.fullscreenResolutions[resolutionManager.fullscreenResolutions.Count - 1].y + " (Fullscreen)");


        menutoadd.AddOptions(options);
        menutoadd.value = options.Count - 1;
    }
}
