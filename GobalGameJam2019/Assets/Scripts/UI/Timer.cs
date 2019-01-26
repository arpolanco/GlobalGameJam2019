using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float mTimeLeft = 0;
    private float mTime = 0;
    private bool mRunning = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mRunning)
            if (mTimeLeft >= 0)
                mTimeLeft -= Time.deltaTime;
    }

    public void StartTimer()
    {
        mRunning = true;
    }


    public void StopTimer()
    {
        mRunning = false;
    }

    public void ResetTimer()
    {
        mTimeLeft = mTime;
    }

    public void SetTimer(float seconds)
    {
        mTime = seconds;
    }

    public bool IsDone()
    {
        return mTimeLeft <= 0;
    }

    public float CheckTime()
    {
        return mTimeLeft;
    }


}
