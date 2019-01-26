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
    public GameObject mPlayerMonster;
    public GameObject mEnemyMonster;




    public GameObject mPlayerMonsterMonsterInfo;
    public GameObject mEnemyMonsterMonsterInfo;

    void Start()
    {
        mCurrentPhase = Phase.PRE;
        StartBattle(mPlayerMonster, mEnemyMonster);
    }

    //This will have info like current party member and such later but for now it will just use the one test monster
    void StartBattle(GameObject player, GameObject enemy)
    {
        mPlayer= player;
        mEnemy = enemy;
        mPlayerMonster = player;
        mEnemyMonster = enemy;
        mPlayerMonsterMonsterInfo.GetComponent<MonsterStatus>().SetMonster(player.GetComponent<Entity>());
        mEnemyMonsterMonsterInfo.GetComponent<MonsterStatus>().SetMonster(enemy.GetComponent<Entity>());
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
                            mPlayerMonster.GetComponent<Animator>().SetTrigger("ATK1");
                            mPlayerMonster.GetComponent<Entity>().Attack(mEnemyMonster.GetComponent<Entity>());
                            break;

                        case ButtonAction.ATK2:
                            mPlayerMonster.GetComponent<Animator>().SetTrigger("ATK2");
                            mPlayerMonster.GetComponent<Entity>().Attack(mEnemyMonster.GetComponent<Entity>());
                            break;

                        case ButtonAction.ATK3:
                            mPlayerMonster.GetComponent<Animator>().SetTrigger("ATK3");
                            mPlayerMonster.GetComponent<Entity>().Attack(mEnemyMonster.GetComponent<Entity>());
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
                mEnemyMonster.GetComponent<Animator>().SetTrigger("ATK1");
                mEnemyMonster.GetComponent<Entity>().Attack(mPlayerMonster.GetComponent<Entity>());
                break;

            case 1:
                mEnemyMonster.GetComponent<Animator>().SetTrigger("ATK2");
                mEnemyMonster.GetComponent<Entity>().Attack(mPlayerMonster.GetComponent<Entity>());
                break;

            case 2:
                mEnemyMonster.GetComponent<Animator>().SetTrigger("ATK3");
                mEnemyMonster.GetComponent<Entity>().Attack(mPlayerMonster.GetComponent<Entity>());
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
            Debug.Log(mCurrentPhase);

            if (Input.GetKeyDown(KeyCode.N))
                ++mCurrentPhase;
            if (mCurrentPhase > Phase.OPPONENT_ATTACK)
                mCurrentPhase = Phase.BEGIN;
        }


        if (mPlayerMonster.GetComponent<Entity>().GetHP() <= 0)
            mCurrentPhase = Phase.PLAYER_SWITCH;

        if (mEnemyMonster.GetComponent<Entity>().GetHP() <= 0)
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
