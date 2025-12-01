namespace cGUI.Math;

public partial struct GUIMath
{
    public static int Clamp(int value, int min, int max)
    {
        if (value < min)
        {
            value = min;
        }
        else if (value > max)
        {
            value = max;
        }

        return value;
    }

    public static float Clamp(float value, float min, float max)
    {
        if (value < min)
        {
            value = min;
        }
        else if (value > max)
        {
            value = max;
        }

        return value;
    }

    public static float Lerp(float a, float b, float t)
    {
        return a + (b - a) * Clamp(t, 0, 1);
    }
}
