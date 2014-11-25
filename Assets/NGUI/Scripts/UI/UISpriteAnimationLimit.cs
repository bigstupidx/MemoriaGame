using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Very simple sprite animation. Attach to a sprite and specify a common prefix such as "idle" and it will cycle through them.
/// </summary>

[ExecuteInEditMode]
[RequireComponent(typeof(UISprite))]
[AddComponentMenu("NGUI/UI/Sprite Animation Limit")]
public class UISpriteAnimationLimit : MonoBehaviour
{
    [HideInInspector][SerializeField] protected int mFPS = 30;
    [HideInInspector][SerializeField] protected string mPrefix = "";
    [HideInInspector][SerializeField] protected int mMinSprite = 0;
    [HideInInspector][SerializeField] protected int mMaxSprite = 1;
    [HideInInspector][SerializeField] protected bool mReverse = false;
    [HideInInspector][SerializeField] protected bool mLoop = true;
    [HideInInspector][SerializeField] protected bool mSnap = true;
    [HideInInspector] public List<EventDelegate> onFinished = new List<EventDelegate>();

    List<EventDelegate> mTemp = null;

    /// <summary>
    /// Convenience function -- set a new OnFinished event delegate (here for to be consistent with RemoveOnFinished).
    /// </summary>

    public void SetOnFinished (EventDelegate.Callback del) { EventDelegate.Set(onFinished, del); }

    /// <summary>
    /// Convenience function -- set a new OnFinished event delegate (here for to be consistent with RemoveOnFinished).
    /// </summary>

    public void SetOnFinished (EventDelegate del) { EventDelegate.Set(onFinished, del); }

    /// <summary>
    /// Convenience function -- add a new OnFinished event delegate (here for to be consistent with RemoveOnFinished).
    /// </summary>

    public void AddOnFinished (EventDelegate.Callback del) { EventDelegate.Add(onFinished, del); }

    /// <summary>
    /// Convenience function -- add a new OnFinished event delegate (here for to be consistent with RemoveOnFinished).
    /// </summary>

    public void AddOnFinished (EventDelegate del) { EventDelegate.Add(onFinished, del); }

    /// <summary>
    /// Remove an OnFinished delegate. Will work even while iterating through the list when the tweener has finished its operation.
    /// </summary>

    public void RemoveOnFinished (EventDelegate del)
    {
        if (onFinished != null) onFinished.Remove(del);
        if (mTemp != null) mTemp.Remove(del);
    }

    protected UISprite mSprite;
    protected float mDelta = 0f;
    protected int mIndex = 0;
    protected bool mActive = true;
    protected List<string> mSpriteNames = new List<string>();

    /// <summary>
    /// Number of frames in the animation.
    /// </summary>

    public int frames { get { return mSpriteNames.Count; } }

    /// <summary>
    /// Animation framerate.
    /// </summary>

    public int framesPerSecond { get { return mFPS; } set { mFPS = value; } }

    /// <summary>
    /// Set the name prefix used to filter sprites from the atlas.
    /// </summary>

    public string namePrefix { get { return mPrefix; } set { if (mPrefix != value) { mPrefix = value; RebuildSpriteList(); } } }

    /// <summary>
    /// Set the animation to be looping or not
    /// </summary>

    public bool loop { get { return mLoop; } set { mLoop = value; } }

    /// <summary>
    /// Returns is the animation is still playing or not
    /// </summary>

    public bool isPlaying { get { return mActive; } }

    /// <summary>
    /// Rebuild the sprite list first thing.
    /// </summary>

    protected virtual void Awake () { RebuildSpriteList(); }

    /// <summary>
    /// Advance the sprite animation process.
    /// </summary>

    protected virtual void Update ()
    {
        if (mActive && mSpriteNames.Count > 1 && Application.isPlaying && mFPS > 0)
        {
            mDelta += RealTime.deltaTime;
            float rate = 1f / mFPS;

            if (rate < mDelta)
            {

                mDelta = (rate > 0f) ? mDelta - rate : 0f;

                if (mReverse)
                {
                    if (--mIndex < 0)
                    {
                        mIndex = mSpriteNames.Count - 1;
                        mActive = mLoop;
                        CallOnFinished();
                    }
                }
                else
                {
                    if (++mIndex >= mSpriteNames.Count)
                    {
                        mIndex = 0;
                        mActive = mLoop;
                        CallOnFinished();
                    }

                }
              

                if (mActive)
                {
                 
                    mSprite.spriteName = mSpriteNames[mIndex];
                    if (mSnap) mSprite.MakePixelPerfect();
                }
            }
        }
    }
    public void FinishedCall(){
        if (onFinished != null)
        {
            mTemp = onFinished;
            onFinished = new List<EventDelegate>();

            // Notify the listener delegates
            EventDelegate.Execute(mTemp);

            // Re-add the previous persistent delegates
            for (int i = 0; i < mTemp.Count; ++i)
            {
                EventDelegate ed = mTemp[i];
                if (ed != null && !ed.oneShot) EventDelegate.Add(onFinished, ed, ed.oneShot);
            }
            mTemp = null;
        }
    }
    protected void CallOnFinished(){
        if (!mActive)
        {
            FinishedCall ();
        }
    }
    /// <summary>
    /// Rebuild the sprite list after changing the sprite name.
    /// </summary>

    public void RebuildSpriteList ()
    {
        if (mSprite == null) mSprite = GetComponent<UISprite>();
        mSpriteNames.Clear();

        if (mSprite != null && mSprite.atlas != null)
        {
            List<UISpriteData> sprites = mSprite.atlas.spriteList;

            for (int i = 0, imax = sprites.Count; i < imax; ++i)
            {
                UISpriteData sprite = sprites[i];

                if (string.IsNullOrEmpty(mPrefix) || sprite.name.StartsWith(mPrefix))
                {
                    string number = sprite.name.Remove(0, mPrefix.Length);

                    if (mMinSprite <= int.Parse(number) && int.Parse(number) <= mMaxSprite)
                    {
                        mSpriteNames.Add(sprite.name);
                    }
                }
            }
            mSpriteNames.Sort();
        }
    }

    /// <summary>
    /// Reset the animation to the beginning.
    /// </summary>

    public void Play () { mActive = true; }

    /// <summary>
    /// Pause the animation.
    /// </summary>

    public void Pause () { mActive = false; }

    /// <summary>
    /// Reset the animation to frame 0 and activate it.
    /// </summary>

    public void ResetToBeginning ()
    {
        mActive = true;
        if (mReverse)
        {
            mIndex = mSpriteNames.Count - 1;
        }
        else
        {
            mIndex = 0;

        }

        if (mSprite != null && mSpriteNames.Count > 0 )
        {
            mSprite.spriteName = mSpriteNames[mIndex];
            if (mSnap) mSprite.MakePixelPerfect();
        }
    }

    public void PlayForward(){
        enabled = true;
        ResetToBeginning();
    }
}

