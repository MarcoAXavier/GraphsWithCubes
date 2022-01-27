using UnityEngine;
using static UnityEngine.Mathf;

public static class FunctionLibrary
{
    static Function[] Functions = new Function[] { Wave, MultiWave, Ripple };

    public enum FunctionName { Wave, MultiWave, Ripple }

    public delegate Vector3 Function(float u, float v, float t);

    public static Function GetFunction(int functionIndex)
    {
        return Functions[functionIndex];
    }

    public static int GetFunctionsCount()
    {
        return Functions.Length;
    }

    public static Vector3 Wave(float u, float v, float t)
    {
        Vector3 p = Vector3.zero;
        p.x = u;
        p.y = Sin(PI * (u + v + t));
        p.z = v;
        return p;
    }
    public static Vector3 MultiWave(float u, float v, float t)
    {
        Vector3 p = Vector3.zero;
        p.x = u;
        p.z = v;

        float y = Sin(PI * (u + t));
        y += 0.5f * Sin(2f * PI * (v + t));
        y += Sin(PI * (u + v + .25f * t));
        p.y = y * (1f / 2.5f);

        return p;
    }
    public static Vector3 Ripple(float u, float v, float t)
    {
        Vector3 p = Vector3.zero;
        p.x = u;
        p.z = v;

        float distance = Sqrt(u*u + v*v);
        float y = Sin(PI*(4 * distance-t))   /   (1f+(10f*distance));
        p.y = y;
        
        return p;
    }
}
