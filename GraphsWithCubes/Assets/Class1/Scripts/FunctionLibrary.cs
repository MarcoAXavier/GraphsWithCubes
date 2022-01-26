using static UnityEngine.Mathf;

public static class FunctionLibrary
{
    static Function[] Functions = new Function[] { Wave, MultiWave, Ripple };

    public enum FunctionName { Wave, MultiWave, Ripple }

    public delegate float Function(float x, float z, float t);

    public static Function GetFunction(int functionIndex)
    {
        return Functions[functionIndex];
    }

    public static int GetFunctionsCount()
    {
        return Functions.Length;
    }

    public static float Wave(float x, float z, float t)
    {
        return Sin(PI * (x + z + t));
    }
    public static float MultiWave(float x, float z, float t)
    {
        float y =  Sin(PI * (x + t));
        y += 0.5f * Sin(2f * PI * (z + t));
        y += Sin(PI * (x + z + .25f * t));
        return y * (1f/2.5f);
    }
    public static float Ripple(float x, float z, float t)
    {
        float distance = Sqrt(x*x + z*z);
        float y = Sin(PI*(4 * distance-t))   /   (1f+(10f*distance));
        return y;
    }
}
