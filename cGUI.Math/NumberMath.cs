namespace cGUI.Math;

public partial struct GUIMath
{
    public static float Ceil(float f)
    {
        return (float) System.Math.Ceiling(f);
    }

    public static float Floor(float f)
    {
        return (float) System.Math.Floor(f);
    }

    public static float Round(float f)
    {
        return (float) System.Math.Round(f);
    }

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

    public static float LerpUnclamped(float a, float b, float t)
    {
        return a + (b - a) * t;
    }

    public static float InverseLerp(float a, float b, float value)
    {
        if (a != b)
        {
            return Clamp((value - a) / (b - a), 0, 1);
        }

        return 0f;
    }

    public static float Min(float a, float b)
    {
        return (a < b) ? a : b;
    }

    public static float Min(params float[] values)
    {
        int num = values.Length;
        if (num == 0)
        {
            return 0f;
        }

        float num2 = values[0];
        for (int i = 1; i < num; i++)
        {
            if (values[i] < num2)
            {
                num2 = values[i];
            }
        }

        return num2;
    }
    public static float Max(float a, float b)
    {
        return (a > b) ? a : b;
    }

    public static float Max(params float[] values)
    {
        int num = values.Length;
        if (num == 0)
        {
            return 0f;
        }

        float num2 = values[0];
        for (int i = 1; i < num; i++)
        {
            if (values[i] > num2)
            {
                num2 = values[i];
            }
        }

        return num2;
    }
}
