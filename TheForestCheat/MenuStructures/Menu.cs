using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ForestCheat.MenuStructures
{
    public class Menu
    {
        public int id;
        private string title;
        public int currentSubMenu = 0;
        public Rect position { get; set; }


        private MenuRenderer renderer;
        private MenuNav nav = new MenuNav();
        private List<SubMenu> subMenus = new List<SubMenu>();

        public float
            pad = 10f,
            buttonWidth,
            buttonY = 20f,
            Headerpad = 40f,
            subMenuSize = 15f,
            buttonHeight = 30f,


            OptionPad = 10f,
            ButtonSize = 40f,
            ToggleSize = 40f,
            SliderSize = 50f,
            initialX
        ;

        public Menu(string name, int width = 400)
        {
            title = name;
            id = IDGen();
            position = new Rect(200, 200, width, 100);

            pad = position.width / pad;
            buttonWidth = subMenus.Count > 1 ? (position.width - (pad * subMenus.Count)) / subMenus.Count : position.width / 2;
            initialX = pad / 2f;

            renderer = new MenuRenderer();
            renderer.menu = this;
            renderer.init();
        }

        public void AddSubMenu(string title = "Not Set")
        {
            subMenus.Add(new SubMenu());
            subMenus.Last().parent = this;
            subMenus.Last().title = title;
            subMenus.Last().padding = OptionPad;
            renderer.setHeight();
            buttonWidth = subMenus.Count > 1 ? (position.width - (pad * subMenus.Count)) / subMenus.Count : position.width / 2;
        }

        public void Update()
        {
            nav.Update();

            foreach (SubMenu menu in subMenus)
                menu.Update();
            //subMenus[currentSubMenu].Update();
        }

        public string GetTitle() => title;
        public MenuNav GetMenuNav() => nav;
        public SubMenu Last() => subMenus.Last();
        public void Render() => renderer.Render();
        public List<SubMenu> GetSubMenus() => subMenus;
        public SubMenu CurrentSubMenu() => subMenus[currentSubMenu];
        private int IDGen() => (int)(Random.Range(0, 100f) + Random.Range(0, 100f));


        public static void ToggleDebug()
        {
            foreach (Menu menu in MenuManager.menuList)
                menu.renderer.debug = !menu.renderer.debug;
        }
    }
}

