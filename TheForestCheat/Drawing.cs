using UnityEngine;

class Drawing : MonoBehaviour
{
    private static Texture2D _staticRectTexture;
    private static GUIStyle _staticRectStyle;

    public static void GUIDrawRect(Rect position, Color color)
    {
        if (_staticRectTexture == null)
            _staticRectTexture = new Texture2D(1, 1);

        if (_staticRectStyle == null)
            _staticRectStyle = new GUIStyle();

        _staticRectTexture.SetPixel(0, 0, color);
        _staticRectTexture.Apply();

        _staticRectStyle.normal.background = _staticRectTexture;

        GUI.Box(position, GUIContent.none, _staticRectStyle);
    }

    //public static Texture2D lineTex;

    public static void DrawLine(Rect rect) => DrawLine(rect, GUI.contentColor, 0.5f); 
    public static void DrawLine(Rect rect, Color color) => DrawLine(rect, color, 0.5f); 
    public static void DrawLine(Rect rect, float width) => DrawLine(rect, GUI.contentColor, width); 
    public static void DrawLine(Rect rect, Color color, float width) => DrawLine(new Vector2(rect.x, rect.y), new Vector2(rect.x + rect.width, rect.y + rect.height), color, width); 
    public static void DrawLine(Vector2 pointA, Vector2 pointB) => DrawLine(pointA, pointB, GUI.contentColor, 0.5f); 
    public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color) => DrawLine(pointA, pointB, color, 0.5f); 
    public static void DrawLine(Vector2 pointA, Vector2 pointB, float width) => DrawLine(pointA, pointB, GUI.contentColor, width); 
    public static void DrawLine(Vector3 start, Vector3 end, Color color, float width)
    {
        if (start == Vector3.zero || end == Vector3.zero) 
            return;

        Vector2 d = end - start;
        float a = Mathf.Rad2Deg * Mathf.Atan((d.y != 0 ? d.y : d.y + 0.01f) / (d.x != 0 ? d.x : d.x + 0.01f));
        if (d.x < 0)
            a += 180;

        int width2 = (int)Mathf.Ceil(width / 2);

        GUIUtility.RotateAroundPivot(a, start);
        GUI.DrawTexture(new Rect(start.x, start.y - width2, d.magnitude, width), Texture2D.whiteTexture);
        GUIUtility.RotateAroundPivot(-a, start);
    }

    public static void DrawBox(Vector3 objPos, float flW, float flH, Color gCol)
    {
        GUIDrawRect(new Rect(objPos.x - flW / 2, objPos.y - flH, 1, flH), gCol);    // Left
        GUIDrawRect(new Rect(objPos.x + flW / 2, objPos.y - flH, 1, flH), gCol);    // Right
        GUIDrawRect(new Rect(objPos.x - flW / 2, objPos.y - flH, flW, 1), gCol);    // Top
        GUIDrawRect(new Rect(objPos.x - flW / 2, objPos.y, flW, 1), gCol);          // Bottom
    }

}