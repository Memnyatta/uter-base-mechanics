using UnityEngine;
public static class MathUtils
{
    public static double LerpExtrapolate(double a, double b, double t)
    {
        return (1 - t) * a + b * t;
    }
    public static double InvLerpExtrapolate(double a, double b, double v)
    {
        return (v - a) / (b - a);
    }
    public static float LerpExtrapolate(float a, float b, float t)
    {
        return (1 - t) * a + b * t;
    }
    public static float InvLerpExtrapolate(float a, float b, float v)
    {
        return (v - a) / (b - a);
    }
    public static Vector3 LerpExtrapolate(Vector3 a, Vector3 b, float t)
    {
        return (1 - t) * a + b * t;
    }
}