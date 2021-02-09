using ForestCheat;
using ForestCheat.MenuStructures;
using TheForest.Utils;
using UnityEngine;

public class ForestHack : MonoBehaviour
{
    public static bool init = false;
    void Start()
    {
        Classes.Update();
        init = Main.Initialization();
    }

    void Update()
    {
        Classes.Update();
        MenuManager.Update();
    }

    void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 100, 100), "Successful Injection:");
        MenuManager.Render();
        ESP.DrawAnimals();
        ESP.DrawEnemies();
        ESP.DrawWorld();
    }
}

