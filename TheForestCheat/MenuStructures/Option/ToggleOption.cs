using UnityEngine;

namespace ForestCheat.MenuStructures.Option
{
    public class ToggleOption : FunctionOption
    {
        private bool state2;
        public ToggleOption(string title) : base(title) { }

        public ToggleOption(string title, bool state, FunctionCall func) : this(title)
        {
            function = func;
            this.state = state;
        }

        public ToggleOption(string title, ref bool state, bool state2) : this(title)
        {
            function = null;
            function_p = null;
            this.state = state;
            this.state2 = state2;
        }

        public ToggleOption(string title, bool state, FunctionCallP func, object param = null) : this(title)
        {
            function_p = func;
            this.state = state;
            arg = param;
        }

        public override void Update()
        {
            if (state)
            {
                if (arg == null)
                    function();
                else
                    function_p(arg);
            }
        }

        public override void Handle()
        {
            Color current = GUI.color;

            GUI.color = state ? color : current;
            if (GUI.Button(new Rect(x, y, width, height), title))
                state = !state;

            GUI.color = current;
        }
    }
}
