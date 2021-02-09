using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static ForestCheat.MenuStructures.Option.FunctionOption;
using static ForestCheat.MenuStructures.Option.SliderOption;

namespace ForestCheat.MenuStructures
{
    public static class MenuManager
    {
        public static Menu LastMenu() => menuList.Last();
        public static List<Menu> menuList = new List<Menu>();
        public static void Add(Menu menu) => menuList.Add(menu);
        public static SubMenu LastSubMenu() => menuList.Last().Last();
        public static void Add(string title) => LastMenu().AddSubMenu(title);
        public static void Add(string title, FunctionCall func) => LastSubMenu().Add(title, func);
        public static void Add(string title, bool state, FunctionCall func) => LastSubMenu().Add(title, state, func);
        public static void Add(string title, Action<Vector3> func, KeyCode key) => LastSubMenu().Add(title, func, key);
        public static void Add(string title, FunctionCallP func, object param) => LastSubMenu().Add(title, func, param);
        public static void Add(string title, bool state, FunctionCallP func, object param) => LastSubMenu().Add(title, state, func, param);
        public static void Add(string title, FunctionCallS func, float param, float left, float right) => LastSubMenu().Add(title, func, param, left, right);

        public static void Update()
        {
            foreach (Menu menu in menuList)
                menu.Update();
        }

        public static void Render()
        {
            foreach (Menu menu in menuList)
                menu.Render();
        }

    }
}
