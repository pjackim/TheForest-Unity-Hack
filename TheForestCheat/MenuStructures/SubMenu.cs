using System.Collections.Generic;
using System;
using UnityEngine;
using ForestCheat.MenuStructures.Option;
using static ForestCheat.MenuStructures.Option.FunctionOption;
using static ForestCheat.MenuStructures.Option.SliderOption;

namespace ForestCheat.MenuStructures
{
    public class SubMenu
    {
        private List<BaseOption> options = new List<BaseOption>();
        private bool init = true;
        private float height = 30f;
        public Menu parent { get; set; }
        public string title { get; set; }
        public float padding { get; set; }

        public void SubMenuCB(int ID) //Loop Options
        {
            foreach (BaseOption option in options)
                option.Handle();
        }

        public void Update() //Loop Options
        {
            if (init && ForestHack.init)
            {
                UpdateOptionPosition();
                init = false;
            }

            foreach (BaseOption option in options)
                option.Update();
        }

        public void Render() //Loop Options
        {
/*            foreach (BaseOption option in options)
                option.Render();*/
        }

        private void UpdateOptionPosition() //Does not need to be called every frame
        {
            if (options.Count == 0) return;

            options[0].setPos(parent.OptionPad, parent.OptionPad + 20);
            options[0].width = 150;

            for (int i = 1; i < options.Count; i++)
            {
                float lasty = options[i - 1].y;
                float lasth = options[i - 1].height;

                options[i].setPos(parent.OptionPad, lasty + lasth + parent.OptionPad);
                options[i].width = 150;
            }
        }

        public float CalcHeight() => height;
        public int Count() => options.Count;
        public void Add(string title, FunctionCall func)
                => OptionHelper( new FunctionOption(title, func), parent.ButtonSize);

        public void Add(string title, FunctionCallP func, object param) 
                => OptionHelper(new FunctionOption(title, func, param), parent.ButtonSize);

        public void Add(string title, bool state, FunctionCall func)
                => OptionHelper(new ToggleOption(title, state, func), parent.ToggleSize);

        public void Add(string title, bool state, FunctionCallP func, object param)
                => OptionHelper(new ToggleOption(title, state, func), parent.ToggleSize);

        public void Add(string title, FunctionCallS func, float value, float left, float right)
                => OptionHelper(new SliderOption(title, func, value, left, right), parent.SliderSize);

        public void Add(string title, Action<Vector3> func, KeyCode key)
                => OptionHelper(new RayCastOption(title, func, key), parent.ButtonSize);

        public void Add(string title, Action<Vector3> func, Vector3 origin, Vector3 direction, KeyCode key)
                => OptionHelper(new RayCastOption(title, func, origin, direction, key), parent.ButtonSize);

        private void OptionHelper(BaseOption option, float size)
        {
            height += size + parent.OptionPad;
            option.height = size;
            options.Add(option);
        }
    }
}
