using System;
using static ESP;
using UnityEngine;
using static Classes;
using TheForest.Utils;
using ForestCheat.MenuStructures;
using static ForestCheat.Cheats.Spawn;
using static ForestCheat.Cheats.Player;
using static ForestCheat.Cheats.Environment;
using static ForestCheat.MenuStructures.Option.SliderOption;
using static ForestCheat.MenuStructures.Option.FunctionOption;


namespace ForestCheat
{
    public class Main
    {
        public static bool Initialization()
        {
            Add(new Menu("Main Menu"));
                Add("General");
                    Add("Update Classes", InitializeClasses);
                    Add("Save Game", Save);
                    Add("Menu Debug", Menu.ToggleDebug);

                Add("RayCast");
                    Add("Mutant", SpawnRandom, KeyCode.RightArrow);
                    Add("Teleport (pos = )", Teleport, KeyCode.RightArrow);

                Add("Player");
                    Add("Give All Items", giveAllItems);
                    Add("God Mode", Health, false);
                    Add("Toggle Fly", togFly);
                    Add("Fly Mode", Fly, 100f, -100f, 500f);
                    Add("Speed", SpeedSlide, FPC.runSpeed, 1f, 999f);
                    Add("Jump", JumpHeight, FPC.jumpHeight, 1f, 300f);

                Add("Visuals");
                    Add("Draw Distance", DrawDistance, 500, 50, 1000);
                    Add("Toggle Distance", DistanceDraw);
                    Add("All ESP", AllEsp);
                    Add("Bone ESP", BoneESP);
                    Add("Joint ESP", JointESP);
                    Add("Animals", AnimalESP);
                    Add("World ESP", WorldESP);
                    Add("Cave", CavesESP);


                Add("Atmos.");
                    Add("Time", TimeChange, Scene.Atmosphere.TimeOfDay, 0, 360);
                    Add("No Fog", NoFog, false);
                    


            Add(new Menu("Spawn Menu"));

                Add("Options");
                    Add("Despawn All", despawnAll);
                    Add("Go to Closest", GoToClosest);
                    Add("Scream", Scream);
                    Add("Found", Found);
                    Add("Change X", cX, 0f, 0f, 100f);
                    Add("Change Y", cY, 1f, 0f, 100f);
                    Add("Change Z", cZ, 3f, 0f, 100f);

                Add("Singles");
                    Add("Male", spawn, Enemy.male);
                    Add("Female", spawn, Enemy.female);
                    Add("Female Skinnny", spawn, Enemy.female_skinny);
                    Add("Male Skinny", spawn, Enemy.male_skinny);
                    Add("Pale", spawn, Enemy.pale);
                    Add("Pale Skinny", spawn, Enemy.pale_skinny);
                    Add("Fireman", spawn, Enemy.fireman);

                Add("Singles 2");
                    Add("Vags", spawn, Enemy.vags);
                    Add("Armsy", spawn, Enemy.armsy);
                    Add("Baby", spawn, Enemy.baby);
                    Add("Fat", spawn, Enemy.fat);
                    Add("Skinned", spawn, Enemy.skinnedMale);
                    Add("Girl", spawn, Enemy.girl);
                    Add("Dynamite Man", spawn, Enemy.dynamiteman);

                Add("Family");
                    Add("Regular", SpawnFamily, Enemy.family_regular);
                    Add("Painted", SpawnFamily, Enemy.family_painted);
                    Add("Skinned", SpawnFamily, Enemy.family_skinned);
                    Add("Skinny", SpawnFamily, Enemy.family_skinny);

                Add("Horde");
                    Add("Easy", SpawnHorde, Enemy.horde_easy);
                    Add("Medium", SpawnHorde, Enemy.horde_medium);
                    Add("Hard", SpawnHorde, Enemy.horde_hard);
                    Add("End", SpawnHorde, Enemy.horde_end);
                    Add("Fireman", SpawnHorde, Enemy.horde_kkk);
                    Add("Baby", SpawnHorde, Enemy.horde_baby);
                    Add("Creepy", SpawnHorde, Enemy.horde_creepy);

            return true;
        }

        public static void Add(Menu menu) => MenuManager.Add(menu);
        public static void Add(string title) => MenuManager.Add(title);
        public static void Add(string title, FunctionCall func) => MenuManager.Add(title, func);
        public static void Add(string title, FunctionCall func, bool state) => MenuManager.Add(title, state, func);
        public static void Add(string title, FunctionCallP func, object param) => MenuManager.Add(title, func, param);
        public static void Add(string title, Action<Vector3> func, KeyCode key = KeyCode.None) => MenuManager.Add(title, func, key);
        public static void Add(string title, FunctionCallP func, object param, bool state) => MenuManager.Add(title, state, func, param);
        public static void Add(string title, FunctionCallS func, float param, float left, float right) => MenuManager.Add(title, func, param, left, right);
    }
}
