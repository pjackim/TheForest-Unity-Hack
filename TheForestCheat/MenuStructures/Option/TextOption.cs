using UnityEngine;

namespace ForestCheat.MenuStructures.Option
{
    public class TextOption : BaseOption
    {
        string display = "", sep = " : ";
        object data;
        public TextOption(string title, object data)
        {
            setPos(0, 0);
            height = 15f;
            display = title;
            this.data = data;
        }
        public override void Exec()
        {
            //throw new NotImplementedException();
        }

        public override void Handle()
        {
            GUI.Label(new Rect(x, y, width, height), display + sep + data.ToString());
        }

        public override void Render()
        {
            //throw new NotImplementedException();
        }

        public override void Update()
        {
            //throw new NotImplementedException();
        }
    }
}
