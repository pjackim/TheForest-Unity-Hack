using System.IO;
using UnityEngine;
using static Classes;
using TheForest.Utils;
using TheForest.Items;
using TheForest.Player.Clothing;
using static TheForest.Items.ItemDatabase;


namespace ForestCheat.Cheats
{
    class Player : MonoBehaviour
    {
        private static long dwLastUpdateTime = 0;
        private static bool bFly = false;

        public static void updateCheats()
        {
            dwLastUpdateTime = Time();
        }

        public static void Save() => playerStats.JustSave();
        public static void togFly() => bFly = !bFly;

        public static void Health()
        {
            float value = 9999f;
            playerStats.Health = value;
            playerStats.Energy = value;
            playerStats.Stamina = value;
            playerStats.Thirst = 0;
            playerStats.Fullness = value;
        }

        public static void Armor()
        {
            for (int i = 0; i < 10; i++)
                playerStats.AddArmorVisible(PlayerStats.ArmorTypes.Creepy);
        }

        public static void Fly(float value)
        {
            LocalPlayer.Transform.position = new Vector3(localPlayer.transform.position.x, value, localPlayer.transform.position.z);
            LocalPlayer.CamFollowHead.stopAllCameraShake();
        }
        public static void SpeedSlide(float value)
        {
            if (FPC != null)
                FPC.runSpeed = value;
        }

        public static void JumpHeight(float value)
        {
            if (FPC != null)
            {
                FPC.jumpHeight = value;
                FPC.allowFallDamage = false;
            }
        }

        public static void Teleport(Vector3 destination)
        {
            Vector3 final = destination;
            final.y += 5;
            LocalPlayer.Transform.position = final;
        }

        public static void giveAllItems()
        {
            foreach (Item item in Items)
            {
                item._maxAmount = 1000;
                item._reloadDuration = .1f;
                localPlayer.AddItem(item._id);
            }
        }


        public static void dumpItems()
        {
            string path = "A:\\Forest Items Dump.txt";
            File.AppendAllText(path, "\n/////////////////////////////////// ITEMS ///////////////////////////////////\n\n");
            foreach (Item item in Items)
            {
                string spaces = "";

                for (int i = 0; i < (30 - item._name.Length); i++)
                    spaces += " ";

                string iteminfo;
                iteminfo = " Name: " + item._name + spaces;
                iteminfo += "\t | ID: " + item._id;
                iteminfo +=  " | Type: " + item._type;

                File.AppendAllText(path, iteminfo + "\n");
            }

            File.AppendAllText(path, "\n/////////////////////////////////// CLOTHING ///////////////////////////////////\n\n");

            foreach (ClothingItem item in ClothingItemDatabase.Items)
            {
                string spaces = "";
                for (int i = 0; i < (30 - item._name.Length); i++)
                    spaces += " ";

                string iteminfo = " Name: " + item._name + spaces + "\t | ID: " + item._id;
                File.AppendAllText(path, iteminfo + "\n");
            }
        }

        
        public static void Update()
        {
            if (Time() - dwLastUpdateTime > 5000)
                updateCheats();
        }
    }
}

