using System;
using static Classes;
using UnityEngine;

namespace ForestCheat.MenuStructures.Option
{
    class RayCastOption : BaseOption
    {
        private RaycastHit hit;
        private Action<Vector3> function;
        private Vector3 origin, direction;
        private KeyCode toggleKey;
        private bool canCall = true;

        private GameObject rect;

        public RayCastOption(string title, Action<Vector3> func) : this(title, func, Vector3.zero, Vector3.zero, KeyCode.RightArrow)
        {
            direction = localPlayer._mainCamTr.forward;
            origin = localPlayer._mainCamTr.position + direction * 4;
        }



        public RayCastOption(string title, Action<Vector3> func, Vector3 origin, Vector3 direction, KeyCode key = KeyCode.None)
        {
            this.title = title;
            setPos(0, 0);

            function = func;
            state = false;
            toggleKey = key;

            this.origin = origin;
            this.direction = direction;
            test();
        }
        public RayCastOption(string title, Action<Vector3> func, KeyCode key) : this(title, func) => toggleKey = key;


        public override void Exec()
        {
            if (state)
            {
                Physics.Raycast(localPlayer._mainCamTr.position + localPlayer._mainCamTr.forward * 4, localPlayer._mainCamTr.forward, out hit);

                while (Utils.Get3dDistance(localPlayer._mainCam.transform.position, hit.point) < 200 && !hit.collider.CompareTag("TerrainMain"))
                    Physics.Raycast(hit.point + localPlayer._mainCamTr.forward * 1, localPlayer._mainCamTr.forward, out hit);

                if (toggleKey != KeyCode.None)
                {
                    if (Input.GetKeyDown(toggleKey) && canCall)
                    {
                        function(hit.point);
                        canCall = false;
                    }
                    else if (!Input.GetKeyDown(toggleKey) && !canCall)
                        canCall = true;
                }
                else
                    function(hit.point);
            }
        }

        public override void Handle()
        {
            Color current = GUI.color;

            GUI.color = state ? color : current;
            if (GUI.Button(new Rect(x, y, width, height), title))
                state = !state;

            
            rect.GetComponent<MeshRenderer>().enabled = state;
            GUI.Label(new Rect(20, 20, 200, 200), "" + rect.GetComponent<MeshRenderer>().sortingLayerName + "\nID: " + rect.GetComponent<MeshRenderer>().sortingLayerID);
            GUI.color = current;
        }

        public override void Render()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            Exec();
            rect.transform.position = hit.point;
        }

        private void test()
        {
            if (rect == null)
            {
                rect = GameObject.CreatePrimitive(PrimitiveType.Cube);
                rect.AddComponent<MeshRenderer>();
                rect.GetComponent<MeshRenderer>().enabled = true;
                rect.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(0f, 0.5f, 0.5f, .5f));
                rect.GetComponent<Collider>().enabled = false;

                rect.AddComponent<LOD_Trees>();


                Vector3 scale = rect.transform.localScale;
                scale.y = 400;
                scale.x = 4;
                scale.z = 4;
                rect.transform.localScale = scale;
                rect.transform.position = localPlayer.transform.position;
            }
        }
        
    }
}
