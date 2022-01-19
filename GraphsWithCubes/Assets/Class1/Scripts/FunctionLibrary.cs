using static UnityEngine.Mathf;

public static class FunctionLibrary
{
    static Function[] Functions = new Function[] { Wave, DoubleWave, Ripple };

    public enum FunctionName { Wave, DoubleWave, Ripple }

    public delegate float Function(float x, float t);
    
    
    public static Function GetFunction(int functionIndex)
    {
        return Functions[functionIndex];
    } 
    public static float Wave(float x, float t)
    {
        return Sin(PI * (x + t));
    }

    public static float DoubleWave(float x, float t)
    {
        float y =  Sin(PI * (x + t));
        y += 0.5f * Sin(2f * PI * (x + t));
        return y * (2f/3f);
    }
    public static float Ripple(float x, float t)
    {
        float distance = Abs(x);
        float y = Sin(PI*(4 * distance-t))   /   (1f+(10f*distance));
        return y;
    }
}
