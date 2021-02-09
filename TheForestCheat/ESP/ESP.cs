using PathologicalGames;
using System;
using TheForest.Utils;
using UnityEngine;
using static Classes;
using static Drawing;

class ESP : MonoBehaviour
{
    private static Color caveColor = Color.white;
    private static Color planeColor = Color.cyan;
    private static Color sinkHoleColor = Color.red;

    private static float lineSize = 1f, drawDistance = 500f;
    private static bool
                        box = false,
                        cave = false,
                        allESP = true,
                        boneESP = true,
                        worldESP = true,
                        jointESP = false,
                        animalESP = false,
                        bDistance = false
        ;

    private static HumanBodyBones[] bones =
        {
            HumanBodyBones.Head,            //0
            HumanBodyBones.Neck,            //1
            HumanBodyBones.Chest,           //2
            HumanBodyBones.LeftShoulder,    //3
            HumanBodyBones.LeftUpperArm,    //4
            HumanBodyBones.LeftLowerArm,    //5
            HumanBodyBones.LeftHand,        //6
            HumanBodyBones.RightShoulder,   //7
            HumanBodyBones.RightUpperArm,   //8
            HumanBodyBones.RightLowerArm,   //9
            HumanBodyBones.RightHand,       //10
            HumanBodyBones.Hips,            //11
            HumanBodyBones.LeftUpperLeg,    //12
            HumanBodyBones.LeftLowerLeg,    //13
            HumanBodyBones.LeftFoot,        //14
            HumanBodyBones.RightUpperLeg,   //15
            HumanBodyBones.RightLowerLeg,   //16
            HumanBodyBones.RightFoot        //17
        };
    private static void DrawBox(Vector3 objPos, float flW, float flH, Color gCol)
    {
        GUIDrawRect(new Rect(objPos.x - flW / 2, objPos.y - flH, 1, flH), gCol);    // Left
        GUIDrawRect(new Rect(objPos.x + flW / 2, objPos.y - flH, 1, flH), gCol);    // Right
        GUIDrawRect(new Rect(objPos.x - flW / 2, objPos.y - flH, flW, 1), gCol);    // Top
        GUIDrawRect(new Rect(objPos.x - flW / 2, objPos.y, flW, 1), gCol);          // Bottom
    }

    public static void BoxESP() => box = !box;
    public static void CavesESP() => cave = !cave;
    public static void AllEsp() => allESP = !allESP;
    public static void BoneESP() => boneESP = !boneESP;
    public static void JointESP() => jointESP = !jointESP;
    public static void WorldESP() => worldESP = !worldESP;
    public static void AnimalESP() => animalESP = !animalESP;
    public static void DistanceDraw() => bDistance = !bDistance;
    public static void DrawDistance(float value) => drawDistance = value;


    public static void DrawEnemies()
    {
        if (allESP)
        {
            foreach (GameObject obj in Scene.MutantControler.activeWorldCannibals)
            {
                mutantScriptSetup character = obj.GetComponentInChildren<mutantScriptSetup>();

                if ((boneESP || jointESP) && DistanceToPlayer(obj.transform) < drawDistance)
                    DrawSkeleton(character.animator);

               
                if (box)
                {
                    Vector3 position = character.transform.position;
                    Vector3 head = position;
                    head.y += character.controller == null ? 5 : character.controller.height;

                    Vector3 screenPosition = Utils.manualWorldToScreenPoint(position);
                    Vector3 screenHead = Utils.manualWorldToScreenPoint(head);
                    float distance = Utils.Get3dDistance(mainCam.transform.position, position);
                    string display = "" + Mathf.Floor(distance) + "m";

                    if (screenPosition != Vector3.zero)
                    {
                        float enemyHeight = screenPosition.y - screenHead.y;
                        float enemyWidth = enemyHeight / 1.5f;

                        GUI.color = Color.green;
                        DrawBox(screenPosition, enemyWidth, enemyHeight, Color.green);

                        Vector2 ObjSize = GUI.skin.GetStyle("label").CalcSize(new GUIContent(display));
                        GUI.Label(new Rect(screenPosition.x - ObjSize.x / 2 + 1, screenHead.y - 17, 500, 40), display);
                    }
                }
            }
        }
    }

    private static void DrawSkeleton(Animator cannibal)
    {
        if (cannibal == null || !cannibal.isHuman || !cannibal.isInitialized) 
            return;

        Vector3[] bonePosition = new Vector3[bones.Length];

        for (int i = 0; i < bones.Length; i++)
        {
            Transform pos = cannibal.GetBoneTransform(bones[i]);

            if (pos != null)
                bonePosition[i] = Utils.manualWorldToScreenPoint(pos.position);
            else
                return;
        }

        Color boxColor = Color.green;
        Color lineColor = Color.red;
        Color prev = GUI.color;
        float boxSize = 10;

        if (jointESP)
        {
            GUI.color = boxColor;
            DrawBox(bonePosition[0], boxSize, boxSize, boxColor);
            DrawBox(bonePosition[2], boxSize, boxSize, boxColor);

            DrawBox(bonePosition[3], boxSize, boxSize, boxColor);
            DrawBox(bonePosition[4], boxSize, boxSize, boxColor);
            DrawBox(bonePosition[5], boxSize, boxSize, boxColor);
            DrawBox(bonePosition[6], boxSize, boxSize, boxColor);

            DrawBox(bonePosition[7], boxSize, boxSize, boxColor);
            DrawBox(bonePosition[8], boxSize, boxSize, boxColor);
            DrawBox(bonePosition[9], boxSize, boxSize, boxColor);
            DrawBox(bonePosition[10], boxSize, boxSize, boxColor);

            DrawBox(bonePosition[11], boxSize, boxSize, boxColor);

            DrawBox(bonePosition[12], boxSize, boxSize, boxColor);
            DrawBox(bonePosition[13], boxSize, boxSize, boxColor);
            DrawBox(bonePosition[14], boxSize, boxSize, boxColor);

            DrawBox(bonePosition[15], boxSize, boxSize, boxColor);
            DrawBox(bonePosition[16], boxSize, boxSize, boxColor);
            DrawBox(bonePosition[17], boxSize, boxSize, boxColor);
        }

        if (boneESP)
        {
            GUI.color = lineColor;
            DrawLine(bonePosition[0], bonePosition[1], lineColor, lineSize);
            DrawLine(bonePosition[1], bonePosition[2], lineColor, lineSize);

            DrawLine(bonePosition[2], bonePosition[3], lineColor, lineSize);
            DrawLine(bonePosition[2], bonePosition[7], lineColor, lineSize);

            DrawLine(bonePosition[3], bonePosition[4], lineColor, lineSize);
            DrawLine(bonePosition[4], bonePosition[5], lineColor, lineSize);
            DrawLine(bonePosition[5], bonePosition[6], lineColor, lineSize);

            DrawLine(bonePosition[7], bonePosition[8], lineColor, lineSize);
            DrawLine(bonePosition[8], bonePosition[9], lineColor, lineSize);
            DrawLine(bonePosition[9], bonePosition[10], lineColor, lineSize);

            DrawLine(bonePosition[2], bonePosition[11], lineColor, lineSize);
            DrawLine(bonePosition[11], bonePosition[12], lineColor, lineSize);
            DrawLine(bonePosition[11], bonePosition[15], lineColor, lineSize);

            DrawLine(bonePosition[12], bonePosition[13], lineColor, lineSize);
            DrawLine(bonePosition[13], bonePosition[14], lineColor, lineSize);

            DrawLine(bonePosition[15], bonePosition[16], lineColor, lineSize);
            DrawLine(bonePosition[16], bonePosition[17], lineColor, lineSize);
        }
        GUI.color = prev;
    }

    public static void DrawAnimals()
    {
        if (animalESP && allESP)
        {
            foreach (Transform animal in PoolManager.Pools["creatures"])
            {
                
                float distanceToAnimal = DistanceToPlayer(animal);
                Vector3 position = Utils.manualWorldToScreenPoint(animal.position);

                GUI.color = Color.green;
                if (position != Vector3.zero && distanceToAnimal <= drawDistance)
                {
                    float height = Mathf.Pow(distanceToAnimal, -1.0f) * 100 + 10;
                    height = height > 50 ? 50 : height;
                    

                    string name =  animal.GetRootGameObject().name;
                    name = name.Substring(0, name.IndexOf('G'));
                    name = name.Substring(0, 1).ToUpper() + name.Substring(1);

                    Vector3 labelPos = position;
                    labelPos.x = height * 0.60f + 3;
                    labelPos.y = position.y - height - 5;

                    DrawBox(position, height * 0.60f, height, Color.green);
                    DrawAtPoint(position, name);
                }
            }
        }
    }

    public static void DrawWorld()
    {
        if (!allESP) 
            return;

        Color prev = GUI.color;
        if (worldESP)
        {
            GUI.color = sinkHoleColor;
            DrawAtPoint(Scene.SinkHoleCenter.position, "SinkHole");

            GUI.color = planeColor;
            DrawAtPoint(Scene.SceneTracker.planeCrash.transform.position, "✈");

            GUI.color = Color.green;
            DrawAtPoint(Scene.Yacht.transform.position, "Yacht");
        }
        
        if (cave)
        {
            GUI.color = caveColor;
            foreach (caveEntranceManager cave in Scene.SceneTracker.caveEntrances)
                DrawAtPoint(cave.transform.position, "Cave");
        }
        GUI.color = prev;
    }

    public static void DrawAtPoint(Vector3 position, string text)
    {
        float distance = Mathf.Floor(DistanceToPlayer(position));
        Vector3 point = Utils.manualWorldToScreenPoint(position);

        if (point == Vector3.zero || distance >= drawDistance)
            return;

        text += bDistance ? "\n" + distance + "m" : "";
        GUI.Label(new Rect(point.x, point.y, 100, 100), text);
    }

    private static float DistanceToPlayer(Transform position) => DistanceToPlayer(position.position);
    private static float DistanceToPlayer(Vector3 position) => Utils.Get3dDistance(LocalPlayer.Transform.position, position);
}
