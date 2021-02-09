using UnityEngine;

namespace ForestCheat.MenuStructures.Option
{
    public class SliderOption : BaseOption
    {
        public float LeftValue, RightValue, increment;
        public delegate void FunctionCallS(float argument);
        public FunctionCallS function_s;
        private float prev_value;
        public SliderOption(string title, FunctionCallS func, float param, float left, float right)
        {
            this.title = title;
            setPos(0, 0);
            //setHeight(30f);
            value = param;
            LeftValue = left;
            RightValue = right;
            function_s = func;
            state = false;
            increment = RightValue - LeftValue > 10 ? 1f : 0.1f;
            prev_value = value;
        }

        public override void Exec() => function_s(value);

        public override void Handle() //This could be optimized by checking if I need to exec()
        {
            GUI.Label(new Rect(x, y, width, height), title + ":\t" + Mathf.Floor(value));

            if (GUI.Button(new Rect(x, y + height / 2, 20, 20), "<"))
                value = (value - increment) >= LeftValue ? value - increment : value;

            if (GUI.Button(new Rect(x + 25 + width + 5, y + height / 2, 20, 20), ">"))
                value = (value + increment) <= RightValue ? value + increment : value;

            state = GUI.Toggle(new Rect(x + 25 + width + 5, y + height / 2 - 20, 15, 15), state, "");

            value = GUI.HorizontalSlider(new Rect(x + 25, y + height / 2 + 5, width, height), value, LeftValue, RightValue);

            if (prev_value != value)
            {
                Exec();
                prev_value = value;
            }
        }

        public override void Update()
        {
            if (state)
                Exec();
        }

        public override void Render()
        {
            
        }

        public void setHeight(float value) => height = value + 15f;
    }
}
