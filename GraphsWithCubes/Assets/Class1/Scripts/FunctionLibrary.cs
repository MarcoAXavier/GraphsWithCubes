using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public static class FunctionLibrary
{
    static List<Function> Functions = new List<Function>()
    {
        Wave, MultiWave, Ripple, Sphere, PerturbedSphere, SphericMandala, Torus, PerturbedTorus
    };

    public enum FunctionName
    {
        Wave, MultiWave, Ripple, Sphere, PerturbedSphere, SphericMandala, Torus, PerturbedTorus
    }

    public delegate Vector3 Function(float u, float v, float t);

    public static Function GetFunction(int functionIndex)
    {
        return Functions[functionIndex];
    }

    public static string GetFunctionName(Function function)
    {
        var functionName = (FunctionName)Functions.IndexOf(function);
        return functionName.ToString();
    }

    public static Function GetNextFunction(Function function)
    {
        int functionIndex = Functions.Contains(function) ? Functions.IndexOf(function) : -1;
        int nextFunctionIndex = (functionIndex + 1) % Functions.Count;
        return Functions[nextFunctionIndex];
    }

    public static Function GetRandomFunctionOtherThan(Function excludedFunction)
    {
        int rnd = -1;
        for (int i = 0; i < 5; i++)
        {
            rnd = Random.Range(0, Functions.Count);
            if (rnd != Functions.IndexOf(excludedFunction)) break;
        }
        return GetFunction(rnd);
    }

    public static Vector3 Morph(float u, float v, float t, Function from, Function to, float morphT)
    {
        var smoothMorthT = SmoothStep(0, 1, morphT);
        return Vector3.LerpUnclamped(from(u, v, t), to(u, v, t), smoothMorthT);
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

    public static Vector3 Torus(float u, float v, float t)
    {
        float biggerRadius = 0.75f;
        float smallerRadius = 0.25f;
        float scale = biggerRadius + smallerRadius * Cos( PI * v);
        Vector3 p = Vector3.zero;
        p.x = scale * Sin(PI * u + t);
        p.y = smallerRadius * Sin(PI * v);
        p.z = scale * Cos(PI * u + t);
        return p;
    }

    public static Vector3 PerturbedTorus(float u, float v, float t)
    {
        float biggerRadius = 0.7f + 0.1f * Sin(PI * (6f * u + 0.5f * t));
        float smallerRadius = 0.15f + 0.05f * Sin(PI * (8f * u + 4f * v + 2f * t));
        float scale = biggerRadius + smallerRadius * Cos(PI * v);
        Vector3 p = Vector3.zero;
        p.x = scale * Sin(PI * u + t);
        p.y = smallerRadius * Sin(PI * v);
        p.z = scale * Cos(PI * u + t);
        return p;
    }
}
