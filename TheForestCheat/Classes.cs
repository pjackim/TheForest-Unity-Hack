using System;
using TheForest.Utils;
using UnityEngine;

class Classes : MonoBehaviour
{
    private static bool init = true;
    private static long dwLastUpdateTime = 0;

    public static Scene scene = null;
    public static Camera mainCam = null;
    public static animalAI[] animals = null;
    public static spawnMutants spawner = null;
    public static LocalPlayer localPlayer = null;
    public static PlayerStats playerStats = null;
    public static FirstPersonCharacter FPC = null;
    public static mutantScriptSetup[] mutantScripts = null;
    public static SceneColorControl_Anim colorControl = null;
    public static CharacterController[] characterControllers = null;

    public static void InitializeClasses()
    {
        GameObject camObject = GameObject.Find("MainCamNew");
        scene = FindObjectOfType<Scene>();
        mainCam = camObject.GetComponent<Camera>();
        localPlayer = FindObjectOfType<LocalPlayer>();
        playerStats = FindObjectOfType<PlayerStats>();
        FPC = FindObjectOfType<FirstPersonCharacter>();
        colorControl = FindObjectOfType<SceneColorControl_Anim>();


        init = false;
    }

    private static void UpdateClasses()
    {
        animals = FindObjectsOfType<animalAI>();
        dwLastUpdateTime = Time(); 
    }

    public static void Update()
    {
        if (Time() - dwLastUpdateTime > 5000)
            UpdateClasses();

        if (init)
            InitializeClasses();
    }

    public static long Time() => DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
}