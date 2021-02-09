using UnityEngine;
using System;
using static Classes;
using TheForest.Utils;
using PathologicalGames;
using Rand = UnityEngine.Random;

namespace ForestCheat.Cheats
{
    class Spawn : MonoBehaviour
    {
        public static GameObject EnemySpawner = null, ItemSpawner = null;
        private static long dwLastUpdateTime = 0;
        private static Enemy last;

        public enum Enemy
        {
            male = 0,
            female = 1,
            female_skinny = 2,
            male_skinny = 3,
            pale = 4,
            pale_skinny = 5,
            fireman = 6,
            vags = 7,
            armsy = 8,
            baby = 9,
            fat = 10,
            skinnedMale = 11,

            family_skinny = 12,
            family_regular = 13,
            family_painted = 14,
            family_skinned = 15,

            horde_easy = 16,
            horde_medium = 17,
            horde_hard = 18,
            horde_end = 19,

            horde_baby = 20,
            horde_kkk = 21,
            horde_creepy = 22,

            girl = 100,
            dynamiteman = 101
        }

        static float dx = 0, dy = 1, dz = 3;
        public static void cX(float value) => dx = value;
        public static void cY(float value) => dy = value;
        public static void cZ(float value) => dz = value;
        public static void despawnAll() => PoolManager.Pools["enemies"].DespawnAll();
        public static void GoToClosest() => localPlayer.transform.position = Scene.MutantControler.findClosestEnemy(localPlayer.transform).position;

        //Spawn Items
        public static void spawn(int itemID, Vector3 position) => spawn(itemID, position, Vector3.zero);
        public static void spawn(int itemID, Vector3 position, Vector3 positionOffset)
        {
            Vector3 spawnLocation = position + positionOffset;
            Quaternion spawnRotation = UnityEngine.Random.rotation;
        }


        //Spawn Enemies
        public static void spawn(object type) => spawn((Enemy)type, 1, localPlayer.transform.position, new Vector3(dx, dy, dz));
        public static void spawn(Enemy type, Vector3 position) => spawn(type, 1, position, Vector3.zero);
        public static void spawn(Enemy type, int amount, Vector3 position) => spawn(type, amount, position, Vector3.zero);
        public static void spawn(Enemy type, int amount, Vector3 position, Vector3 positionOffset)
        {
            Vector3 spawnLocation = position + positionOffset;

            //Transform player = localPlayer.transform;
            //Vector3 spawnLocation = player.position + player.forward * positionOffset.x;
            //spawnLocation = spawnLocation + player.up * positionOffset.y;


            Quaternion spawnRotation = UnityEngine.Random.rotation;
            EnemySpawner = Instantiate(Resources.Load<GameObject>("instantMutantSpawner"), spawnLocation, spawnRotation);
            AddEnemy(ref EnemySpawner, type, amount);
            EnemySpawner.GetComponent<spawnMutants>().range = 2f;
            EnemySpawner.GetComponent<spawnMutants>().SendMessage("doSpawn");
            Reset(ref EnemySpawner);
        }

        public static void SpawnRandom(Vector3 pos) => spawn((Enemy)Rand.Range(0, 11), pos);
        public static void SpawnFamily(object type) => SpawnFamily((Enemy)type, localPlayer.transform.position + new Vector3(0, 1, 5), 1f);

        private static void SpawnFamily(Enemy type, Vector3 position, float difficulty = 1f)
        {
            Vector3 spawnLocation = position;
            Quaternion spawnRotation = UnityEngine.Random.rotation;
            GameObject GO = Instantiate(Resources.Load<GameObject>("instantMutantSpawner"), spawnLocation, spawnRotation);
            spawnMutants spawner = GO.GetComponent<spawnMutants>();

            spawner.instantSpawn = true;
            spawner.sleepingSpawn = false;

            switch (type)
            {
                case Enemy.family_painted:
                    Reset(ref GO);
                    AddEnemy(ref GO, Enemy.male, Mathf.CeilToInt(4 * difficulty));
                    AddEnemy(ref GO, Enemy.female, Mathf.CeilToInt(1 * difficulty));
                    spawner.leader = true;
                    spawner.paintedTribe = true;
                    spawner.pale = false;
                    break;

                case Enemy.family_regular:
                    Reset(ref GO);
                    AddEnemy(ref GO, Enemy.male, Mathf.CeilToInt(4 * difficulty));
                    AddEnemy(ref GO, Enemy.female, Mathf.CeilToInt(1 * difficulty));
                    spawner.leader = true;
                    spawner.pale = false;
                    break;

                case Enemy.family_skinned:
                    Reset(ref GO);
                    AddEnemy(ref GO, Enemy.pale, Mathf.CeilToInt(2 * difficulty));
                    AddEnemy(ref GO, Enemy.pale_skinny, Mathf.CeilToInt(3 * difficulty));
                    spawner.leader = true;
                    spawner.paintedTribe = false;
                    spawner.pale = true;
                    spawner.skinnedTribe = true;
                    break;

                case Enemy.family_skinny:
                    Reset(ref GO);
                    AddEnemy(ref GO, Enemy.female_skinny, Mathf.CeilToInt(2 * difficulty));
                    AddEnemy(ref GO, Enemy.male_skinny, Mathf.CeilToInt(3 * difficulty));
                    spawner.leader = true;
                    spawner.pale = false;
                    break;

                case Enemy.horde_kkk:
                    Reset(ref GO);
                    AddEnemy(ref GO, Enemy.fireman, Mathf.CeilToInt(4 * difficulty));
                    spawner.leader = true;
                    spawner.paintedTribe = true;
                    spawner.pale = true;
                    spawner.skinnedTribe = true;
                    break;

                case Enemy.horde_baby:
                    Reset(ref GO);
                    AddEnemy(ref GO, Enemy.baby, Mathf.CeilToInt(10 * difficulty));
                    break;

                case Enemy.horde_creepy:
                    Reset(ref GO);
                    AddEnemy(ref GO, Enemy.fat, Mathf.CeilToInt(2 * difficulty));
                    AddEnemy(ref GO, Enemy.vags, Mathf.CeilToInt(1 * difficulty));
                    AddEnemy(ref GO, Enemy.armsy, Mathf.CeilToInt(1 * difficulty));
                    break;
            }
            spawner.SendMessage("doSpawn");
            Reset(ref GO);
        }
        private static void AddEnemy(ref GameObject obj, Enemy type, int amount)
        {
            spawnMutants component = obj.GetComponent<spawnMutants>();
            switch (type)
            {
                case Enemy.male:
                    component.amount_male = amount;
                    break;

                case Enemy.female:
                    component.amount_female = amount;
                    break;

                case Enemy.female_skinny:
                    component.amount_female_skinny = amount;
                    break;

                case Enemy.male_skinny:
                    component.amount_male_skinny = amount;
                    break;
                case Enemy.pale:
                    component.amount_pale = amount;
                    break;

                case Enemy.pale_skinny:
                    component.amount_skinny_pale = amount;
                    break;

                case Enemy.fireman:
                    component.amount_fireman = amount;
                    break;

                case Enemy.vags:
                    component.amount_vags = amount;
                    break;

                case Enemy.armsy:
                    component.amount_armsy = amount;
                    break;

                case Enemy.baby:
                    component.amount_baby = amount;
                    break;

                case Enemy.fat:
                    component.amount_fat = amount;
                    break;

                case Enemy.skinnedMale:
                    component.amount_pale = amount;
                    component.skinnedTribe = true;
                    component.pale = true;
                    break;

                case Enemy.girl:
                    component.amount_girl = amount;
                    break;

                case Enemy.dynamiteman:
                    component.amount_fireman = amount;
                    component.useDynamiteMan = true;
                    break;
            }
        }
        public static void SpawnHorde(object type)
        {
            last = (Enemy) type;
            float minDistance = 75, maxDistance = 150;
            Transform player = localPlayer.transform;

            float distance = UnityEngine.Random.Range(minDistance, maxDistance);
            Vector3 location = player.position;

            for (int i = 0; i < 5; i++)
            {
                switch (i)
                {
                    case 0: //Front
                        location = player.position + player.forward * distance;
                        break;
                    case 1: //Right
                        location = player.position + player.right * distance;
                        break;
                    case 2: //Left
                        location = player.position - player.right * distance;
                        break;
                    case 3: //Front Right
                        location = player.position + (player.forward * (distance / 2) + (player.right * (distance / 2)));
                        break;
                    case 4: //Front Left
                        location = player.position + (player.forward * (distance / 2) - (player.right * (distance / 2)));
                        break;

                }

                Vector2 circle = UnityEngine.Random.insideUnitCircle;
                circle.Normalize();
                circle = circle * UnityEngine.Random.Range(5f, 15f);
                location.x += circle.x;
                location.z += circle.y;
                location.y += 50;

                RaycastHit Ground;
                Physics.Raycast(location, Vector3.down, out Ground, 100f);
                if (!Ground.collider.CompareTag("TerrainMain"))
                    continue;

                switch ((Enemy)type)
                {
                    case Enemy.horde_easy:
                        switch (UnityEngine.Random.Range(0, 1))
                        {
                            case 0:
                                spawn(Enemy.male_skinny, Ground.point);
                                break;
                            case 1:
                                spawn(Enemy.female_skinny, Ground.point);
                                break;
                        }
                        break;

                    case Enemy.horde_medium:
                        break;

                    case Enemy.horde_hard:
                        break;

                    case Enemy.horde_end:
                        break;

                    case Enemy.horde_baby:
                        spawn(Enemy.baby, 4, Ground.point);
                        break;

                    case Enemy.horde_kkk:
                        spawn(Enemy.fireman, Ground.point);
                        break;

                    case Enemy.horde_creepy:
                        switch (UnityEngine.Random.Range(0, 3))
                        {
                            case 0:
                                spawn(Enemy.armsy, Ground.point);
                                break;
                            case 1:
                                spawn(Enemy.armsy, Ground.point);
                                break;
                            case 2:
                                spawn(Enemy.fat, Ground.point);
                                break;
                            case 3:
                                spawn(Enemy.vags, Ground.point);
                                break;
                        }
                        break;
                }
            }
        }
        private static void HordeUpdate()
        {
            foreach (GameObject obj in Scene.MutantControler.activeWorldCannibals)
            {
                if (Utils.Get3dDistance(localPlayer.transform.position, obj.transform.position) > 200 || (Scene.MutantControler.activeWorldCannibals.Count < 30 && Utils.Get3dDistance(localPlayer.transform.position, obj.transform.position) > 100))
                    PoolManager.Pools["enemies"].Despawn(obj.transform);

                if (Scene.MutantControler.activeWorldCannibals.Count < 15 && dwLastUpdateTime != 0)
                    SpawnHorde(last);
            }
            Found();
            dwLastUpdateTime = Time();
        }
        public static void Found()
        {
            foreach (GameObject obj in Scene.MutantControler.activeWorldCannibals)
            {
                mutantScriptSetup mutantScript = obj.GetComponentInChildren<mutantScriptSetup>();

                if (mutantScript == null || mutantScript.search == null || mutantScript.pmCombatScript == null || mutantScript.targetFunctions == null || mutantScript.dayCycle == null || mutantScript.aiManager == null)
                    continue;

                mutantScript.lastSighting.transform.position = localPlayer.transform.position;
                mutantScript.search.StartCoroutine(mutantScript.search.findPerpToEnemy(1));
                mutantScript.search.setToWaypoint();
                mutantScript.search.setToPlayer();

                mutantScript.familyFunctions.sendTargetSpotted();

                mutantScript.targetFunctions.defaultVisionRange = 200;
                mutantScript.targetFunctions.threatRemoveDist = 200f;
                mutantScript.targetFunctions.sendAddAttacker();

                if ((bool)mutantScript.followerFunctions)
                    mutantScript.followerFunctions.sendAttackEvent();
            }

            foreach (GameObject obj in Scene.MutantControler.allCreepySpawns)
            {
                mutantScriptSetup mutantScript = obj.GetComponentInChildren<mutantScriptSetup>();

                if (mutantScript == null || mutantScript.search == null || mutantScript.pmCombatScript == null || mutantScript.targetFunctions == null || mutantScript.dayCycle == null || mutantScript.aiManager == null)
                    continue;

                mutantScript.lastSighting.transform.position = localPlayer.transform.position;
                //mutantScript.search.currentTarget = localPlayer;
                mutantScript.search.StartCoroutine(mutantScript.search.findPerpToEnemy(1));
                mutantScript.search.setToWaypoint();

                mutantScript.search.setToPlayer();

                mutantScript.targetFunctions.defaultVisionRange = 400;
                mutantScript.targetFunctions.threatRemoveDist = 400f;
                mutantScript.targetFunctions.sendAddAttacker();

                if ((bool)mutantScript.followerFunctions)
                    mutantScript.followerFunctions.sendAttackEvent();
            }
        }
        public static void Scream()
        {
            foreach (GameObject obj in Scene.MutantControler.activeWorldCannibals)
            {
                mutantScriptSetup mutantScript = obj.GetComponentInChildren<mutantScriptSetup>();
                mutantScript.search.SendMessage("resetScreamCooldown");
                mutantScript.search.playSearchScream();
            }
        }
        private static void Reset(ref GameObject obj)
        {
            spawnMutants component = obj.GetComponent<spawnMutants>();
            component.amount_male = 0;
            component.amount_female = 0;
            component.amount_female_skinny = 0;
            component.amount_male_skinny = 0;
            component.amount_pale = 0;
            component.amount_skinny_pale = 0;
            component.amount_fireman = 0;
            component.amount_vags = 0;
            component.amount_armsy = 0;
            component.amount_baby = 0;
            component.amount_fat = 0;
        }

        public static void Begin()
        {
            //spawnHorde(Enemy.horde_creepy);
        }
        public static void Setup()
        {
            //Player.setFog();
            //despawnAll();
            //Player.Health();
        }

        public static void Update()
        {
            /*            if (Time() - dwLastUpdateTime > 5000)
                            hordeUpdate();*/
        }
    }
}
