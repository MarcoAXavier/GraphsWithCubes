using static UnityEngine.Mathf;

public static class FunctionLibrary
{
    public enum Functions { Wave = 0, DoubleWave = 1, Ripple = 2 }
    
    public static float GetFunction(Functions functionIndex, float x, float t)
    {
        switch ((int)functionIndex)
        {
            case 0:
                return Wave(x, t);
            case 1:
                return DoubleWave(x, t);
            case 2:
                return Ripple(x, t);
            default:
                return float.PositiveInfinity;
        }
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
