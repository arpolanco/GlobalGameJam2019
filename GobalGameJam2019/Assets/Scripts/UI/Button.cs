using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ButtonAction { ATK1, ATK2, ATK3, SWAP}

public class Button : MonoBehaviour 
{
    
    public string mText;
    public Element mElement;

    private Vector3 mOriginal;
    public float mTargetScale = 1;
    private Timer mTimer;


    public ButtonAction mAction;


    public Sprite mFire;
    public Sprite mNormie;
    public Sprite mWater;
    public Sprite mGrass;

    public Sprite mOther;

    // Start is called before the first frame update
    void Start()
    {
        mTargetScale = 1;  
        mOriginal = transform.gameObject.GetComponent<RectTransform>().localScale;
        mTimer = transform.gameObject.GetComponent<Timer>();
        mTimer.SetTimer(.5f);
    }

    // Update is called once per frame
    void Update()
    {

        switch (mElement)
        {
            case Element.FIRE:
                gameObject.GetComponent<Image>().sprite = mFire;
                break;
            case Element.NORMIE:
                gameObject.GetComponent<Image>().sprite = mNormie;
                break;
            case Element.WATER:
                gameObject.GetComponent<Image>().sprite = mWater;
                break;
            case Element.GRASS:
                gameObject.GetComponent<Image>().sprite = mGrass;
                break;
            default:
                gameObject.GetComponent<Image>().sprite = mOther;
                break;
        }

        gameObject.GetComponentInChildren<Text>().text = mText;



        //if cooldown not over
        if (!mTimer.IsDone())
        {
       
            mTargetScale = .9f;
            
        }
        else
        {
            mTargetScale = 1f;
            mTimer.StopTimer();
      
        }

        Vector3 cur = transform.gameObject.GetComponent<RectTransform>().localScale;
        transform.gameObject.GetComponent<RectTransform>().localScale = Vector3.Lerp(cur, mOriginal * mTargetScale, Time.deltaTime * 4f);

    }

    public void Click()
    {
        if (mTimer.IsDone())
        {
            mTimer.ResetTimer();
            mTimer.StartTimer();
            //Debug.Log("click");
        }
    }

    public void Hover()
    {
        if (mTimer.IsDone())
        {
            mTargetScale = 1.1f;
            Vector3 cur = transform.gameObject.GetComponent<RectTransform>().localScale;
            transform.gameObject.GetComponent<RectTransform>().localScale = Vector3.Lerp(cur, mOriginal * mTargetScale, Time.deltaTime * 3f);
        }
    }

}

