using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Phase {BEGIN, PLAYER_CHOICE, PLAYER_ATTACK, OPPONENT_CHOICE, OPPONENT_ATTACK };


public class BattlePhaseManager : MonoBehaviour
{

    private Phase mCurrentPhase;

    [SerializeField, Range(0,10)]
    public float mStartPhaseAnimationDuration;

    private float mCurStartPhaseAnimationDuration = 0;


    // Start is called before the first frame update
    void Start()
    {
        mCurrentPhase = Phase.BEGIN;
    }


    // Update is called once per frame
    void Update()
    {
        //Allows us to test the other battle phases 
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.N))
                ++mCurrentPhase;
            if (mCurrentPhase > Phase.OPPONENT_ATTACK)
                mCurrentPhase = Phase.BEGIN;
        }

        
    }

    void AnimateBegin()
    {

    }


    void InitializeUI()
    {

    }

    
}
