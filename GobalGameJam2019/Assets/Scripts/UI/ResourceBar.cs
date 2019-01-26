using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBar : MonoBehaviour
{

    private float mMaxResource = 100;
    private float mCurResource = 100;

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

        if (Input.GetKeyDown(KeyCode.A))
            SetCurrentResource(mCurResource - 10);

        if (Input.GetKeyDown(KeyCode.S))
            SetCurrentResource(mCurResource + 10);

        if (mCurShakeDur >= 0) {
            mCurShakeDur += Time.deltaTime*5;
            mResourceForeground.localScale = Vector3.Lerp(mResourceForeground.localScale, new Vector3(mCurResource / mMaxResource, 1, 1), mCurShakeDur / 10);
            if (mReduced)
            {
                //shake here
            }
        }


        if(mResourceForeground.localScale.x <= 0)
        {
            mResourceForeground.localScale = new Vector3(0, 1, 1);
        }
        

       
        
        
    }

    public void SetMaxResource(float newmax)
    {
        mMaxResource = newmax;
    }

    public void SetCurrentResource(float newresource)
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
        mCurShakeDur = 0;
    }


}
