//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2014 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Tween the object's position.
/// </summary>

public class TweenPosition_2 : UITweener
{
    public Vector3 from;
    public Vector3 to;

    [HideInInspector]
    public bool worldSpace = false;

    Transform mTrans;

    public Transform cachedTransform {
        get {
            if (mTrans == null)
                mTrans = transform;
            return mTrans;
        }
    }

    [System.Obsolete ("Use 'value' instead")]
    public Vector3 position { get { return this.value; } set { this.value = value; } }

    /// <summary>
    /// Tween's current value.
    /// </summary>

    public Vector3 value {
        get {
            return worldSpace ? cachedTransform.position : cachedTransform.localPosition;
        }
        set {
            if (worldSpace)
                cachedTransform.position = value;
            else
                cachedTransform.localPosition = value;
         
        }
    }

    /// <summary>
    /// Tween the value.
    /// </summary>

    protected override void OnUpdate (float factor, bool isFinished)
    {
        value = from * (1f - factor) + to * factor;
    }

    /// <summary>
    /// Start the tweening operation.
    /// </summary>

    static public TweenPosition_2 Begin (GameObject go, float duration, Vector3 pos)
    {
        TweenPosition_2 comp = UITweener.Begin<TweenPosition_2> (go, duration);
        comp.from = comp.value;
        comp.to = pos;

        if (duration <= 0f) {
            comp.Sample (1f, true);
            comp.enabled = false;
        }
        return comp;
    }

    /// <summary>
    /// Start the tweening operation.
    /// </summary>

    static public TweenPosition_2 Begin (GameObject go, float duration, Vector3 pos, bool worldSpace)
    {
        TweenPosition_2 comp = UITweener.Begin<TweenPosition_2> (go, duration);
        comp.worldSpace = worldSpace;
        comp.from = comp.value;
        comp.to = pos;

        if (duration <= 0f) {
            comp.Sample (1f, true);
            comp.enabled = false;
        }
        return comp;
    }

    [ContextMenu ("Set 'From' to current value")]
    public override void SetStartToCurrentValue ()
    {
        from = value;
    }

    [ContextMenu ("Set 'To' to current value")]
    public override void SetEndToCurrentValue ()
    {
        to = value;
    }

    [ContextMenu ("Assume value of 'From'")]
    void SetCurrentValueToStart ()
    {
        value = from;
    }

    [ContextMenu ("Assume value of 'To'")]
    void SetCurrentValueToEnd ()
    {
        value = to;
    }
}
