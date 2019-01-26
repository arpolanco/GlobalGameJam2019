using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Phase {PRE,BEGIN, PLAYER_CHOICE, PLAYER_ATTACK, OPPONENT_CHOICE, OPPONENT_ATTACK };


public class BattlePhaseManager : MonoBehaviour
{

    private Phase mCurrentPhase;

    [SerializeField, Range(0,10)]
    public float mStartPhaseAnimationDuration;

    private float mCurStartPhaseAnimationDuration = 0;


    public GameObject mStartPhaseGUIObject;


    //todo:make this private
    //this will be set at the start of any battle
    public GameObject mPlayer;
    public GameObject mEnemy;



    public GameObject mPlayerMonsterInfo;
    public GameObject mEnemyMonsterInfo;

    void Start()
    {
        mCurrentPhase = Phase.PRE;
        StartBattle(mPlayer, mEnemy);
    }

    //This will have info like current party member and such later but for now it will just use the one test monster
    void StartBattle(GameObject player, GameObject enemy)
    {
        mPlayer = player;
        mEnemy = enemy;
        mPlayerMonsterInfo.GetComponent<MonsterStatus>().SetMonster(player.GetComponent<Entity>());
        mEnemyMonsterInfo.GetComponent<MonsterStatus>().SetMonster(enemy.GetComponent<Entity>());
    }

    //may move this
    void UIControl()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);
        foreach(RaycastResult res in raycastResults)
        {
            if (res.gameObject.GetComponent<Button>() != null)
            {
                Button button = res.gameObject.GetComponent<Button>();
                button.Hover();
                if (Input.GetMouseButtonDown(0))
                {
                    button.Click();

                    //ACTUAL ATTACK SHIT
                    switch(button.mAction)
                    {
                        case ButtonAction.ATK1:
                            mPlayer.GetComponent<Animator>().SetTrigger("ATK1");
                            mPlayer.GetComponent<Entity>().Attack(mEnemy.GetComponent<Entity>());
                            break;

                        case ButtonAction.ATK2:
                            mPlayer.GetComponent<Animator>().SetTrigger("ATK2");
                            mPlayer.GetComponent<Entity>().Attack(mEnemy.GetComponent<Entity>());
                            break;

                        case ButtonAction.ATK3:
                            mPlayer.GetComponent<Animator>().SetTrigger("ATK3");
                            mPlayer.GetComponent<Entity>().Attack(mEnemy.GetComponent<Entity>());
                            break;


                    }

                }

            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        UIControl();

        //Allows us to test the other battle phases 
        if (Application.isEditor)
        {
            //Debug.Log(mCurrentPhase);

            if (Input.GetKeyDown(KeyCode.N))
                ++mCurrentPhase;
            if (mCurrentPhase > Phase.OPPONENT_ATTACK)
                mCurrentPhase = Phase.BEGIN;
        }


        switch (mCurrentPhase)
        {
            case Phase.PRE:
                break;

            case Phase.BEGIN:
                mCurStartPhaseAnimationDuration += Time.deltaTime;
                if (mCurStartPhaseAnimationDuration >= mStartPhaseAnimationDuration)
                {
                    mCurrentPhase = Phase.PLAYER_CHOICE;
                }
                break;

            case Phase.PLAYER_CHOICE:
               
                break;

        }

        
        
        
    }

    
}
