using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Utils;
using UnityEngine;

namespace ForestCheat.Cheats
{
    class Environment
    {
        public static void intensity(float value) => Scene.Atmosphere.DaySunIntensity = value;
        public static void reflect(float value) => TheForestAtmosphere.ReflectionAmount = value;
        public static void ambient(float value) => Scene.Atmosphere.DayAmbientIntensity = value;
        public static void shapeHardness(float value) => Scene.Atmosphere.ShapeHardness = value;
        public static void LightTemp(float value) => Scene.Atmosphere.Sun.colorTemperature = value;
        public static void DaySunBounceIntensity(float value) => Scene.Atmosphere.DaySunBounceIntensity = value;
        public static void FogStartDistance(float value) => Scene.Atmosphere.FogStartDistance = value;
        public static void shapeIntensity(float value) => Scene.Atmosphere.ShapeIntensity = value;
        public static void caveLight(float value) => Scene.Atmosphere.CaveAmbientIntensity = value;
        public static void LDOT(float value) => Scene.Atmosphere.LdotUp = value;
        public static void TimeChange(float value) => Scene.Atmosphere.TimeOfDay = value;


        static Color customOrange = new Color(.588f, 0.314f, .078f);
        static Color customMustard = new Color(.71f, 0.627f, .094f);
        static Color customRed = new Color(.27f, 0.137f, 0.137f);
        static Color customDarkerRed = new Color(.17f, 0.12f, 0.12f);

        static Color fogOrange = new Color(.588f, 0.314f, .078f);
        static Color fogMustard = new Color(.71f, 0.627f, .094f);
        static Color fogRed = new Color(.27f - .117f, 0.137f - .117f, 0.137f - .117f);
        static Color fogDarkerRed = new Color(.17f, 0.12f, 0.12f);
        //static Color fog = new Color

        public static void adjMustard(float value) => fogMustard = new Color(.71f + value, 0.627f + value, .094f + value);
        public static void adjRed(float value) => fogRed = new Color(.27f + value, 0.137f + value, 0.137f + value);
        public static void adjDarkRed(float value) => fogDarkerRed = new Color(.17f + value, 0.12f + value, .012f + value);
        public static void adjOrange(float value) => fogOrange = new Color(.588f + value, 0.314f + value, .078f + value);
        public static void setFog()
        {
            //Scene.Atmosphere.TimeOfDay
            TheForestAtmosphere atmos = Scene.Atmosphere;

            //Fog
            atmos.CurrentFogColor = fogOrange;
            atmos.FogColor = fogOrange;
            atmos.NoonFogColor = fogRed;
            atmos.SunsetFogColor = fogDarkerRed;
            atmos.NightFogColor = Color.black;


            //Day
            atmos.CurrentSunsetLightColor = customOrange;
            atmos.NoonLightColor = customMustard;
            atmos.DayColorEq = atmos.DayColor = atmos.DayColorGround = customMustard;
            atmos.SunsetColor = atmos.SunsetColorEq = atmos.SunsetColorGround = customOrange;

            //Night
            atmos.Moon.color = customRed;
            atmos.NightColor = customRed;
            atmos.NightColorEq = customRed;
            atmos.NightAltColor = customRed;
        }

        public static void Fog()
        {
            Scene.Atmosphere.FoldoutFog = false;
            Scene.Atmosphere.FogCurrent = 19.7f;
            Scene.Atmosphere.FogStartDistance = 1f;
        }

        public static void NoFog() => Scene.Atmosphere.FogStartDistance = 999f;
    }
}
