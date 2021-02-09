using UnityEngine;

namespace ForestCheat.MenuStructures.Option
{
    public abstract class BaseOption
    {
        public float x { get; set; }
        public float y { get; set; }
        public float height { get; set; }
        public float width { get; set; }
        public float value { get; set; }
        public string title { get; set; }
        public bool state { get; set; }
        public Color color = new Color(0.0f, 180.0f, 180.0f);


        public abstract void Exec();

        public abstract void Render();

        public abstract void Handle();

        public abstract void Update();

        public void setPos(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public void setPos(Vector2 point) => setPos(point.x, point.y);


    }
}
