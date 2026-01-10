using System;
using UnityEngine;

namespace cGUI.Animations;

public enum EaseType
{
    Linear, SmoothStep, InQuad, OutQuad, InOutCubic,
    InSine, OutSine, InOutSine,
    InExpo, OutExpo, InOutExpo,
    InCirc, OutCirc, InOutCirc,
    InElastic, OutElastic, InOutElastic,
    InBack, OutBack, InOutBack,
    InBounce, OutBounce, InOutBounce
}

public class StateTween<T>(float durationIn, float durationOut, Func<T, T, float, T> lerpFunc, EaseType easing) where T : struct
{
    private readonly Func<T, T, float, T> m_LerpFunc = lerpFunc;

    private readonly float m_InSpeed = 1f / durationIn;
    private readonly float m_OutSpeed = 1f / durationOut;
    private readonly EaseType m_Easing = easing;

    private float m_Progress = 0f;

    public float Progress => m_Progress;

    public void Update(bool targetState, float deltaTime)
    {
        float speed = targetState ? m_InSpeed : m_OutSpeed;
        float direction = targetState ? 1f : -1f;

        m_Progress = Mathf.Clamp01(m_Progress + direction * speed * deltaTime);
    }

    public T Evaluate(T start, T end)
    {
        return m_LerpFunc(start, end, ApplyEasing(m_Progress, m_Easing));
    }

    private float ApplyEasing(float t, EaseType type)
    {
        return type switch
        {
            EaseType.Linear => t,
            EaseType.SmoothStep => t * t * (3f - 2f * t),
            EaseType.InQuad => t * t,
            EaseType.OutQuad => t * (2f - t),
            EaseType.InOutCubic => t < 0.5f ? 4f * t * t * t : 1f - Mathf.Pow(-2f * t + 2f, 3f) / 2f,

            EaseType.InSine => 1f - Mathf.Cos((t * Mathf.PI) / 2),
            EaseType.OutSine => Mathf.Sin((t * Mathf.PI) / 2),
            EaseType.InOutSine => -(Mathf.Cos(Mathf.PI * t) - 1) / 2,

            EaseType.InExpo => t == 0 ? 0 : Mathf.Pow(2, 10 * (t - 1)),
            EaseType.OutExpo => t == 1 ? 1 : 1 - Mathf.Pow(2, -10 * t),
            EaseType.InOutExpo => t == 0 ? 0 : t == 1 ? 1 : t < 0.5f ? Mathf.Pow(2, 20 * t - 10) / 2 : (2 - Mathf.Pow(2, -20 * t + 10)) / 2,

            EaseType.InCirc => -(Mathf.Sqrt(1 - t * t) - 1),
            EaseType.OutCirc => Mathf.Sqrt(1 - Mathf.Pow(t - 1, 2)),
            EaseType.InOutCirc => t < 0.5f ? (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * t, 2))) / 2 : (Mathf.Sqrt(1 - Mathf.Pow(-2 * t + 2, 2)) + 1) / 2,

            EaseType.InElastic => t == 0 ? 0 : t == 1 ? 1 : -Mathf.Pow(2, 10 * t - 10) * Mathf.Sin((t * 10 - 10.75f) * ((2 * Mathf.PI) / 3)),
            EaseType.OutElastic => t == 0 ? 0 : t == 1 ? 1 : Mathf.Pow(2, -10 * t) * Mathf.Sin((t * 10 - 0.75f) * ((2 * Mathf.PI) / 3)) + 1,
            EaseType.InOutElastic => t == 0 ? 0 : t == 1 ? 1 : t < 0.5f ? -(Mathf.Pow(2, 20 * t - 10) * Mathf.Sin((20 * t - 11.125f) * ((2 * Mathf.PI) / 4.5f))) / 2 : (Mathf.Pow(2, -20 * t + 10) * Mathf.Sin((20 * t - 11.125f) * ((2 * Mathf.PI) / 4.5f))) / 2 + 1,

            EaseType.InBack => (1.70158f + 1) * t * t * t - 1.70158f * t * t,
            EaseType.OutBack => 1 + (1.70158f + 1) * Mathf.Pow(t - 1, 3) + 1.70158f * Mathf.Pow(t - 1, 2),
            EaseType.InOutBack => t < 0.5f ? (Mathf.Pow(2 * t, 2) * (((1.70158f * 1.525f) + 1) * 2 * t - (1.70158f * 1.525f))) / 2 : (Mathf.Pow(2 * t - 2, 2) * (((1.70158f * 1.525f) + 1) * (t * 2 - 2) + (1.70158f * 1.525f)) + 2) / 2,

            _ => t
        };
    }
}