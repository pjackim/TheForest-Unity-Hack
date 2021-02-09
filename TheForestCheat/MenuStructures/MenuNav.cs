using UnityEngine;

namespace ForestCheat.MenuStructures
{
    public class MenuNav
    {
        public bool isOpen, wasInMenu;
        public KeyCode openMenuKey;

        public MenuNav()
        {
            isOpen = false;
            openMenuKey = KeyCode.Insert;
            wasInMenu = !TheForest.Utils.Input.IsMouseLocked;
        }
        public void Update()
        {
            if (Input.GetKeyDown(openMenuKey))
            {
                if (!isOpen && !TheForest.Utils.Input.IsMouseLocked)
                {
                    isOpen = !isOpen;
                    wasInMenu = true;
                    return;
                }
                if (!isOpen && TheForest.Utils.Input.IsMouseLocked)
                {
                    isOpen = !isOpen;
                    wasInMenu = false;
                    TheForest.Utils.Input.UnLockMouse();
                    TheForest.Utils.Input.SetState(TheForest.Utils.InputState.Locked, true);
                    return;
                }
                if (isOpen && wasInMenu)
                {
                    isOpen = !isOpen;
                    return;
                }
                if (isOpen && !wasInMenu)
                {
                    isOpen = !isOpen;
                    TheForest.Utils.Input.LockMouse();
                    TheForest.Utils.Input.SetState(TheForest.Utils.InputState.Locked, false);
                    return;
                }
            }
        }

        public void Render()
        {

        }

        public void setKey(KeyCode key)
        {
            openMenuKey = key;
        }
    }
}
