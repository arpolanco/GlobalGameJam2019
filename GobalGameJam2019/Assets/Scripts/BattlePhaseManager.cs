using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Phase {PRE,BEGIN, PLAYER_CHOICE, PLAYER_ATTACK, OPPONENT_CHOICE, OPPONENT_ATTACK };


public class BattlePhaseManager : MonoBehaviour
{

    private Phase mCurrentPhase;

    [SerializeField, Range(0,10)]
    public float mStartPhaseAnimationDuration;

    private float mCurStartPhaseAnimationDuration = 0;


    public GameObject mStartPhaseGUIObject;


    // Start is called before the first frame update
    void Start()
    {
        mCurrentPhase = Phase.PRE;
    }


    // Update is called once per frame
    void Update()
    {
        //Allows us to test the other battle phases 
        if (Application.isEditor)
        {
            Debug.Log(mCurrentPhase);

            if (Input.GetKeyDown(KeyCode.N))
                ++mCurrentPhase;
            if (mCurrentPhase > Phase.OPPONENT_ATTACK)
                mCurrentPhase = Phase.BEGIN;
        }


        switch (mCurrentPhase)
        {
            case Phase.PRE:
                mCurrentPhase = Phase.BEGIN;
                mStartPhaseGUIObject.SetActive(true);
                mStartPhaseGUIObject.GetComponent<StartPhaseGUI>().StartAnimating(mStartPhaseAnimationDuration / 2);
                break;

            case Phase.BEGIN:
                mCurStartPhaseAnimationDuration += Time.deltaTime;
                if (mCurStartPhaseAnimationDuration >= mStartPhaseAnimationDuration)
                {
                    mCurrentPhase = Phase.PLAYER_CHOICE;
                }
                break;

        }

        
        
        
    }

    void AnimateBegin()
    {

    }


    void InitializeUI()
    {

    }

    
}
