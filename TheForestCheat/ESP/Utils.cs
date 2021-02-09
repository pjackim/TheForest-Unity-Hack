using UnityEngine;
using static Classes;

class Utils : MonoBehaviour
{
    public static float Get3dDistance(Vector3 myCoords, Vector3 enemyCoords)
    {
        return Mathf.Sqrt(
            Mathf.Pow(enemyCoords[0] - myCoords[0], 2.0f) +
            Mathf.Pow(enemyCoords[1] - myCoords[1], 2.0f) +
            Mathf.Pow(enemyCoords[2] - myCoords[2], 2.0f));
    }

    public static float Length2D(float x, float y)
    {
        return Mathf.Sqrt((x * x) + (y * y));
    }

    public static Vector3 manualWorldToScreenPoint(Vector3 wp)
    {
        if (wp == null) return wp;

        Matrix4x4 mat = mainCam.projectionMatrix * mainCam.worldToCameraMatrix;

        Vector4 temp = mat * new Vector4(wp.x, wp.y, wp.z, 1f);

        if (temp.w < 0.1f)
            return Vector3.zero;

        float invw = 1.0f / temp.w;

        temp.x *= invw;
        temp.y *= invw;

        Vector2 Center = new Vector2((.5f * mainCam.pixelWidth), (.5f * mainCam.pixelHeight));

        Center.x += 0.5f * temp.x * mainCam.pixelWidth + 0.5f;
        Center.y -= 0.5f * temp.y * mainCam.pixelHeight + 0.5f;

        return new Vector3(Center.x, Center.y, wp.z);
    }

}