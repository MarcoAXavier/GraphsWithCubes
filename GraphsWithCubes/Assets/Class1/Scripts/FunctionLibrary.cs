using UnityEngine;
using static UnityEngine.Mathf;

public static class FunctionLibrary
{
    static Function[] Functions = new Function[] { Wave, MultiWave, Ripple, Sphere, PerturbedSphere, SphericMandala };

    public enum FunctionName { Wave, MultiWave, Ripple, Sphere, PerturbedSphere, SphericMandala }

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

        float distance = Sqrt(u * u + v * v);
        float y = Sin(PI * (4 * distance - t)) / (1f + (10f * distance));
        p.y = y;

        return p;
    }

    public static Vector3 Sphere(float u, float v, float t)
    {
        float radius = 0.5f + 0.5f;
        float scale = radius * Cos(0.5f * PI * v);
        Vector3 p = Vector3.zero;
        p.x = scale * Sin(PI * u + t);
        p.y = radius * Sin(PI * .5f * v);
        p.z = scale * Cos(PI * u + t);
        return p;
    }

    public static Vector3 PerturbedSphere(float u, float v, float t)
    {
        float radius = 0.9f + 0.1f * Sin(PI * (6f * u + 4f * v + t));
        float scale = radius * Cos(0.5f * PI * v);
        Vector3 p = Vector3.zero;
        p.x = scale * Sin(PI * u);
        p.y = radius * Sin(PI * .5f * v);
        p.z = scale * Cos(PI * u);
        return p;
    }
    public static Vector3 SphericMandala(float u, float v, float t)
    {
        float radius = 0.5f + 0.5f;
        float scale = radius * Cos(0.5f * PI * v);
        Vector3 p = Vector3.zero;
        p.x = scale * Sin(PI * u + t);
        p.y = radius * Sin(PI * .5f * v);
        p.z = scale * Cos(PI * u + t * p.y);
        return p;
    }
}
