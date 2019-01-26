using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Phase {PRE,BEGIN, PLAYER_CHOICE, PLAYER_ATTACK, OPPONENT_CHOICE, OPPONENT_ATTACK, ENEMY_SWITCH, PLAYER_SWITCH, PLAYER_DEATH, ENEMY_DEATH };


public class BattlePhaseManager : MonoBehaviour
{

    private Phase mCurrentPhase;



    //todo:make this private
    //this will be set at the start of any battle
    public GameObject mPlayer;
    public GameObject mEnemy;
    private Entity mPlayerMonster;
    private Entity mEnemyMonster;
    private Animator mPlayerAnim;
    private Animator mEnemyAnim;




    public GameObject mPlayerMonsterMonsterInfo;
    public GameObject mEnemyMonsterMonsterInfo;


    public Button[] mOptions;

    void Start()
    {
        mCurrentPhase = Phase.PRE;
        
    }


    void SetupButtons()
    {
        int i = 0;
        foreach(Button b in mOptions)
        {
            Move move = mPlayerMonster.moveList[i].move;
            b.mText = move.id;
            b.mElement = move.effects[0].element;
            b.mAction = (ButtonAction)i;
            ++i;
        }
    }

    //This will have info like current party member and such later but for now it will just use the one test monster
    public void StartBattle(GameObject player, GameObject enemy)
    {
        mPlayer= player;
        mEnemy = enemy;
        mPlayerMonster = player.gameObject.GetComponent<Party>().GetEntity();
        mEnemyMonster = enemy.gameObject.GetComponent<Party>().GetEntity();
        mPlayerMonsterMonsterInfo.GetComponent<MonsterStatus>().SetMonster(mPlayerMonster);
        mEnemyMonsterMonsterInfo.GetComponent<MonsterStatus>().SetMonster(mEnemyMonster);
        mPlayerAnim = mPlayer.GetComponent<Party>().GetEntity().currentAnimator;
        mEnemyAnim = mEnemy.GetComponent<Party>().GetEntity().currentAnimator;
        SetupButtons();
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

                bool hasAttacked = true;
                if (Input.GetMouseButtonDown(0))
                {
                    button.Click();

                    //ACTUAL ATTACK Stuff
                    switch(button.mAction)
                    {
                        case ButtonAction.ATK1:
                            mPlayerAnim.SetTrigger("ATK1");
                            mPlayerMonster.Attack(mEnemyMonster);
                            break;

                        case ButtonAction.ATK2:
                            mPlayerAnim.SetTrigger("ATK2");
                            mPlayerMonster.Attack(mEnemyMonster);
                            break;

                        case ButtonAction.ATK3:
                            mPlayerAnim.SetTrigger("ATK3");
                            mPlayerMonster.Attack(mEnemyMonster);
                            break;

                        case ButtonAction.SWAP:
                            //TODO:: Add party swap shit here
                            hasAttacked = false;
                            break;


                    }

                    if (hasAttacked)
                        ++mCurrentPhase;

                }

            }
        }

    }

    void AIControl()
    {

        int tmp = Random.Range(0, 3);
        switch (tmp)
        {
            case 0:
                mEnemyAnim.SetTrigger("ATK1");
                mEnemyMonster.Attack(mPlayerMonster);
                break;

            case 1:
                mEnemyAnim.SetTrigger("ATK2");
                mEnemyMonster.Attack(mPlayerMonster);
                break;

            case 2:
                mEnemyAnim.SetTrigger("ATK3");
                mEnemyMonster.Attack(mPlayerMonster);
                break;


        }
        ++mCurrentPhase;
    }

    // Update is called once per frame
    void Update()
    {
        

        //Allows us to test the other battle phases 
        if (Application.isEditor)
        {
            //Debug.Log(mCurrentPhase);

            if (Input.GetKeyDown(KeyCode.N))
                ++mCurrentPhase;
            if (mCurrentPhase > Phase.OPPONENT_ATTACK)
                mCurrentPhase = Phase.BEGIN;
        }


        if (mPlayerMonster.GetHP() <= 0)
            mCurrentPhase = Phase.PLAYER_SWITCH;

        if (mEnemyMonster.GetHP() <= 0)
            mCurrentPhase = Phase.ENEMY_SWITCH;

        switch (mCurrentPhase)
        {
            //Random screen wipe effect like in pokemon here if we have time
            //This will also be where music changes
            case Phase.PRE:
                ++mCurrentPhase;
                break;

            //Will do the camera transition here
            case Phase.BEGIN:

                mCurrentPhase = Phase.PLAYER_CHOICE;
                break;

            case Phase.PLAYER_CHOICE:
                UIControl();
                break;

            case Phase.PLAYER_ATTACK:
                ++mCurrentPhase;
                break;


            case Phase.OPPONENT_CHOICE:
                AIControl();
                break;

            case Phase.OPPONENT_ATTACK:
                ++mCurrentPhase;
                break;

            case Phase.ENEMY_SWITCH:
                break;

            case Phase.PLAYER_SWITCH:
                break;

            case Phase.PLAYER_DEATH:
                break;

            case Phase.ENEMY_DEATH:
                break;

            

        }

        
        
        
    }

    
}
