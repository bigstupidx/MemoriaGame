﻿using UnityEngine;

/// <summary>
/// Tween the object's color.
/// </summary>

[AddComponentMenu("NGUI/Tween/Tween Color Camera")]
public class TweenColorCamera : Tweener
{
    public Color from = Color.white;
    public Color to = Color.white;

    bool mCached = false;
    Camera mWidget;
    Light mLight;

    void Cache ()
    {
        mCached = true;

        mWidget = GetComponent<Camera>();
        mLight = GetComponent<Light>();

    }

    [System.Obsolete("Use 'value' instead")]
    public Color color { get { return this.value; } set { this.value = value; } }

    /// <summary>
    /// Tween's current value.
    /// </summary>

    public Color value
    {
        get
        {
            if (!mCached) Cache();
            if (mWidget != null) return mWidget.backgroundColor;
            if (mLight != null) return mLight.color;
            return Color.black;
        }
        set
        {
            if (!mCached) Cache();
            if (mWidget != null) mWidget.backgroundColor = value;

            if (mLight != null)
            {
                mLight.color = value;
                mLight.enabled = (value.r + value.g + value.b) > 0.01f;
            }
        }
    }

    /// <summary>
    /// Tween the value.
    /// </summary>

    protected override void OnUpdate (float factor, bool isFinished) { value = Color.Lerp(from, to, factor); }

    /// <summary>
    /// Start the tweening operation.
    /// </summary>

    static public TweenColor Begin (GameObject go, float duration, Color color)
    {
        #if UNITY_EDITOR
        if (!Application.isPlaying) return null;
        #endif
        TweenColor comp = UITweener.Begin<TweenColor>(go, duration);
        comp.from = comp.value;
        comp.to = color;

        if (duration <= 0f)
        {
            comp.Sample(1f, true);
            comp.enabled = false;
        }
        return comp;
    }

    [ContextMenu("Set 'From' to current value")]
    public override void SetStartToCurrentValue () { from = value; }

    [ContextMenu("Set 'To' to current value")]
    public override void SetEndToCurrentValue () { to = value; }

    [ContextMenu("Assume value of 'From'")]
    void SetCurrentValueToStart () { value = from; }

    [ContextMenu("Assume value of 'To'")]
    void SetCurrentValueToEnd () { value = to; }
}
