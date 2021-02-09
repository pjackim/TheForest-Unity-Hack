using System.Collections.Generic;
using UnityEngine;

namespace ForestCheat.MenuStructures
{
    public class MenuRenderer
    {
        private Rect pos;
        private int id;
        public bool debug = false;

        public Menu menu { get; set; }
        public Color primary { get; set; }
        public Color secondary { get; set; }

        private List<SubMenu> subs;
        private MenuNav nav;

        public MenuRenderer()
        {
            primary = new Color(0.0f, 180.0f, 180.0f);
            secondary = new Color(23.0f, 23.0f, 23.0f);
        }
        public float buttonWidth = 0;
        public void init()
        {
            if (menu == null) return;

            id = menu.id;
            pos = menu.position;
            subs = menu.GetSubMenus();
            nav = menu.GetMenuNav();

            menu.pad = menu.position.width / menu.pad;
            buttonWidth = subs.Count > 1 ? (menu.position.width - (menu.pad * subs.Count)) / subs.Count : menu.position.width / 2;
            menu.initialX = menu.pad / 2f;
        }

        private void MenuWindowCB(int ID)
        {
            if (subs.Count > 0)
                menu.CurrentSubMenu().SubMenuCB(ID); //Draws buttons / options

            GUI.DragWindow();
        }
        public static int callcount = 0;
        private void SubMenuListCB(int ID)
        {
            for (int i = 0; i < subs.Count; i++)
            {
                if (menu.currentSubMenu == i)  //Colors the subMenu Button
                    GUI.color = primary;
                else
                    GUI.color = secondary;

                //Spaces subMenu buttons  X --> X
                float x = menu.initialX + (menu.buttonWidth + menu.pad) * i;

                if (GUI.Button(new Rect(x, menu.buttonY, menu.buttonWidth, menu.buttonHeight), subs[i].title))
                {
                    //Draws subMenu buttons and sets menu height based on subMenu options.
                    menu.currentSubMenu = i;
                    setHeight();
                }
            }
        }

        public void Render()
        {
            //If nav is not open, don't render
            if (!nav.isOpen) 
                return;                

            GUI.backgroundColor = secondary;
            GUI.color = primary;

            //Menu Window
            menu.position = GUI.Window(id, menu.position, MenuWindowCB, menu.CurrentSubMenu().title);

            //Pins subMenu list to right above menu list
            pos = menu.position;
            pos.y -= (menu.buttonHeight + menu.buttonY) + menu.subMenuSize;
            pos.height = menu.position.y - pos.y - 10f;

            //SubMenu List window
            pos = GUI.Window(id + 1, pos, SubMenuListCB, menu.GetTitle());

            if (debug)
            {
                float
                 debug_x = pos.x + pos.width + 20f,
                 debug_y_init = pos.y,
                 debug_y_last = debug_y_init,
                 debug_spacing = 20f
                 ;

                string tab = "   ";

                string text = "Menu: " + menu.GetTitle() + "(ID: " + menu.id + ")";
                GUI.Label(new Rect(debug_x, debug_y_last, 400, 400), text);
                debug_y_last += debug_spacing;


                text = tab + "Menu Position: (" + menu.position.x + ", " + menu.position.y + ")";
                GUI.Label(new Rect(debug_x, debug_y_last, 400, 400), text);
                debug_y_last += debug_spacing;

                text = tab + "Menu Demensions: (" + menu.position.width + " x " + menu.position.height + ")";
                GUI.Label(new Rect(debug_x, debug_y_last, 400, 400), text);
                debug_y_last += debug_spacing;

                text = tab + "Status:";
                GUI.Label(new Rect(debug_x, debug_y_last, 400, 400), text);
                debug_y_last += debug_spacing;

                Color last = colorToggle(true);
                text = tab + tab + "MenuRenderer";
                GUI.Label(new Rect(debug_x, debug_y_last, 400, 400), text);
                debug_y_last += debug_spacing;

                colorToggle(menu.GetMenuNav() != null);
                text = tab + tab + "MenuNav";
                GUI.Label(new Rect(debug_x, debug_y_last, 400, 400), text);
                debug_y_last += debug_spacing;

                colorToggle(menu.GetMenuNav() != null);
                text = tab + tab + "SubMenu[]";
                GUI.Label(new Rect(debug_x, debug_y_last, 400, 400), text);
                debug_y_last += debug_spacing;
                GUI.color = last;




                text = "SubMenus (" + subs.Count + "):";
                GUI.Label(new Rect(debug_x, debug_y_last, 400, 400), text);
                debug_y_last += debug_spacing;

                for (int i = 0; i < subs.Count; i++)
                {
                    Color c = colorToggle(i == menu.currentSubMenu);
                    text = tab + subs[i].title + tab + "Options: " + subs[i].Count();
                    GUI.Label(new Rect(debug_x, debug_y_last, 400, 400), text);
                    debug_y_last += debug_spacing;
                    GUI.color = c;
                }
            }
        }

        public static Color colorToggle(bool state)
        {
            Color last = GUI.color;
            GUI.color = state ? Color.green : Color.red;
            return last;
        }
        public void setHeight()
        { 
            menu.position = new Rect(menu.position.x, menu.position.y, menu.position.width, subs[menu.currentSubMenu].CalcHeight());
        }
    }
}
