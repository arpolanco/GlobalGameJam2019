using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPhaseGUI : MonoBehaviour
{
    private bool mIsAnimatingIn = false;
    private bool mIsAnimatingOut = false;
    private float mDuration;
    private float mCurDuration;

    /// <summary>
    /// Only using this until we get something fancier
    /// </summary>
    public Text mTextObj;

    private Color mTargetColor;
    private Color mStartColor;
    // Start is called before the first frame update
    void Start()
    {
        mTextObj.color = Color.clear;
        mStartColor = Color.clear;
        mTargetColor = Color.white;

    }

    // Update is called once per frame
    void Update()
    {
        mCurDuration += Time.deltaTime;

        if (mIsAnimatingIn | mIsAnimatingOut)
            mTextObj.color = Color.Lerp(mStartColor, mTargetColor, mCurDuration/mDuration);

        if (mCurDuration >= mDuration)
        {
            if (mIsAnimatingIn)
            {
                mIsAnimatingOut = true;
                mIsAnimatingIn = false;
                mTargetColor = Color.clear;
                mStartColor = Color.white;

            }
            else
            {
                mIsAnimatingOut = false;
                mIsAnimatingIn = false;
                mTextObj.transform.gameObject.SetActive(false);
            }
            mCurDuration = 0;
        }
    }

    public void StartAnimating(float animationDur)
    {
        mIsAnimatingIn = true;
        mDuration = animationDur;
    }
}
