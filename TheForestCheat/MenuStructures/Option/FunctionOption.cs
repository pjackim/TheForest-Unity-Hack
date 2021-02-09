using UnityEngine;

namespace ForestCheat.MenuStructures.Option
{
    public class FunctionOption : BaseOption
    {
        public delegate void FunctionCallP(object argument = null);
        public delegate void FunctionCall();

        public FunctionCall function;
        public FunctionCallP function_p;
        public object arg;

        public FunctionOption(string title)
        {
            this.title = title;
            setPos(0, 0);
        }

        public FunctionOption(string title, FunctionCall func) : this(title) => function = func;

        public FunctionOption(string title, FunctionCallP func, object param) : this(title)
        {
            function_p = func;
            arg = param;
        }
        public override void Exec()
        {
            if (arg == null)
                function();
            else
                function_p(arg);
        }

        public override void Handle()
        {
            if (GUI.Button(new Rect(x, y, width, height), title))
                Exec();
        }

        public override void Render()
        {

        }

        public override void Update()
        {

        }

        public void setHeight(float value) => height = value;
    }
}
