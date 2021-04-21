using System;
using TMPro;
using UnityEngine;

public static class DATA {
    private static bool isInit;
    
    public static void INIT() {
        if (isInit) return;
        Settings.Init();
        Res.Init();
        isInit = true;
    }
}


public struct Tags {
    // Game Management
    public struct GM {
        public struct Menu {
            public static readonly string
                Difficulty = "DifficultyMenu",
                Gameover = "GameoverMenu",
                Settings = "SettingsMenu",
                Pause = "PauseMenu",
                Main = "MainMenu";
        }
        
        public static readonly string
            PlayerController = "PlayerController",
            CursorController = "CursorController",
            GameManager = "GameManager",
            AudioManager = "AudioManager",
            ResolutionManager = "ResolutionManager",
            LoadScreen = "LoadScreen",
            Timer = "Timer",
            HUD = "HUD";
    }
    
    // Environment
    public struct Env {
        public static readonly string
            Ground = "Ground",
            Wall = "Wall",
            FirstBasketCollider = "1",
            SecondBasketCollider = "2";
    }

    public struct Items {
        public static readonly string        
            Basket = "Basket",
            Ball = "Ball";
    }

    public static readonly string
        GameoverText = "GameoverText",
        MainCamera = "MainCamera",
        BallsPool = "BallsPool",
        ThrowBar = "ThrowBar",
        Player = "Player";
}

public struct Tooltips {
    public const string
        InitIsAllowed = "If this flag is true then GameObject this component attached to will be disabled right after \"Init()\" call. " + 
        "If this flag is false the GameObject will wait for \"gameObject.IsInit = true\" from some other gameObject. Can be used for getting " +
        "references to child object before disabling parent GameObject this component attached to.";
}

public struct Settings {
    public static float UserVolume;
    
    public static void Init() {
        UserVolume = 0.5f;
        DifficultyLevel.Init();
    }

    
    public struct DifficultyLevel {
        private static bool isInit;
        public static bool selectedFromMain;
        public static DifficultyLevel Selected, Easy, Medium, Hard, Test;

        public float TimeLeft;
        public int BallsMax, TargetScore;

        public static void Init() {
            if (isInit) return;
            Easy.TimeLeft = 120f;
            Easy.BallsMax = 10;
            Easy.TargetScore = 8;

            Medium.TimeLeft = 60f;
            Medium.BallsMax = 6;
            Medium.TargetScore = 6;

            Hard.TimeLeft = 40f;
            Hard.BallsMax = 3;
            Hard.TargetScore = 4;

            Test.TimeLeft = 115;
            Test.BallsMax = 10;
            Test.TargetScore = 2;

            isInit = true;
        }
    }
}

public struct Res {
    public static void Init() {
        Fonts.Init();
        Music.Init();
    }
    
    
    public struct Prefabs {
        public static readonly string pauseMenu = "Prefabs/Game Management/Pause Menu";
    }
    
    public struct Sprites {
        public static readonly string СursorDot = "Sprites/Cursor/Small Dot";
        public static readonly string СursorHand = "Sprites/Cursor/hand";
        public static readonly string СursorGrab = "Sprites/Cursor/grab";
    }

    public struct Fonts {
        public static TMP_FontAsset balooChettanRegularSDF;
        
        private static readonly string balooChettanRegularSDFPath = "Fonts/Baloo_Chettan/BalooChettan-Regular SDF";

        public static void Init() {
            balooChettanRegularSDF = Resources.Load(balooChettanRegularSDFPath, typeof(TMP_FontAsset)) as TMP_FontAsset;
        }
    }
    public struct Music {
        public static AudioClip MainSoundtrack, HoopsSoundtrack;
        
        private static readonly string MainSoundtrackPath = "Sounds/Music/Main soundtrack";
        private static readonly string HoopsSoundtrackPath = "Sounds/Music/cron_audio_8-bit_modern02";

        public static void Init() {
            MainSoundtrack = Resources.Load(MainSoundtrackPath, typeof(AudioClip)) as AudioClip;
            HoopsSoundtrack = Resources.Load(HoopsSoundtrackPath, typeof(AudioClip)) as AudioClip;
        }
    }
}

public struct Scenes {
    public static readonly string
        Main,
        Load,
        Hoops;
        //Pachinko,
        //Neighborhood;

    static Scenes() {
        Main = SceneManager.GetSceneNameByIndex(0);
        Load = SceneManager.GetSceneNameByIndex(1);
        Hoops = SceneManager.GetSceneNameByIndex(2);
        //Pachinko = SceneManager.GetSceneNameByIndex(3);
        //Neighborhood = SceneManager.GetSceneNameByIndex(4);
    }

    private static string gameToLoad;
    public static string GameToLoad {
        get => gameToLoad;
        set {
            string scenesList = "";
            var scenes = typeof(Scenes).GetFields();
            for (int i = 0; i < scenes.Length; i++) {
                string tmp = i == scenes.Length - 1 ? "." : ", ";
                string.Concat(scenesList, (string) scenes[i].GetValue(null) + tmp);
                if (value == (string) scenes[i].GetValue(null)) gameToLoad = value;
            }

            if (gameToLoad == null) {
                throw new Exception("Attempt of setting \"GameToLoad\" string value to \"" +
                                    value + "\" didn't find matching scene name in \"Scenes\" struct. " +
                                    "Available scenes: " + scenes);
            }
        }
    }
}