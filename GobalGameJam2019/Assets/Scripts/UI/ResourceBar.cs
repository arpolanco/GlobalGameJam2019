using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBar : MonoBehaviour
{

    private float mMaxResource;
    private float mCurResource;

    private float mShakeDur;
    private float mCurShakeDur;

    private bool mReduced = false;

    [SerializeField] RectTransform mResourceForeground;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mCurShakeDur >= 0) {
            mCurShakeDur -= Time.deltaTime;

            if (mReduced)
            {
                //shake here
            }
        }


        mResourceForeground.localScale = Vector3.Lerp(new Vector3(1,1,1), new Vector3( mCurResource/ mMaxResource, 1, 1), 100/100);
        
    }

    void SetCurrentResource(float newresource)
    {
        if(newresource < mCurResource)
        {
            mReduced = true;
        }
        else
        {
            mReduced = false;
        }
        mCurResource = newresource;
        mCurShakeDur = 1;
    }


}
