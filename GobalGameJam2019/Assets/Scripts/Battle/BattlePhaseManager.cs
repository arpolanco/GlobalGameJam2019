using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Linq;

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
    private GameObject mGUI;
    private int playerLosses, enemyLosses;



    public Transform playerTransform, enemyTransform;
    public GameObject mPlayerMonsterMonsterInfo;
    public GameObject mEnemyMonsterMonsterInfo;
    public bool inBattle = false;


    public Button[] mOptions;

    void Start()
    {
        mCurrentPhase = Phase.PRE;
        mGUI = transform.Find("PhaseGui").gameObject;
        mGUI.SetActive(false);
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
        SpawnMonster(player, playerTransform);
        SpawnMonster(enemy, enemyTransform);
        NewMonsterInit();
        mCurrentPhase = Phase.BEGIN;
        playerLosses = mPlayer.GetComponent<Party>().monsterList.Where(x => x.GetHP() == 0).Count();
        enemyLosses = mEnemy.GetComponent<Party>().monsterList.Where(x => x.GetHP() == 0).Count();
    }

    void SpawnMonster(GameObject trainer, Transform transform)
    {
        Party party = trainer.GetComponent<Party>();
        GameObject go = Instantiate(party.GetEntity().prefab, transform.position + party.GetEntity().initalYOffset * Vector3.up, transform.rotation, null) as GameObject;
        party.GetEntity().currentAnimator = go.GetComponent<Animator>();
    }

    void DespawnMonster(Entity entity)
    {
        if(entity.currentAnimator != null)
        {
            DestroyImmediate(entity.currentAnimator.gameObject);
            entity.currentAnimator = null;
        }
    }

    void NewMonsterInit()
    {
        mPlayerMonster = mPlayer.gameObject.GetComponent<Party>().GetEntity();
        mEnemyMonster = mEnemy.gameObject.GetComponent<Party>().GetEntity();
        mPlayerMonsterMonsterInfo.GetComponent<MonsterStatus>().SetMonster(mPlayerMonster);
        mEnemyMonsterMonsterInfo.GetComponent<MonsterStatus>().SetMonster(mEnemyMonster);
        mPlayerAnim = mPlayer.GetComponent<Party>().GetEntity().currentAnimator;
        mEnemyAnim = mEnemy.GetComponent<Party>().GetEntity().currentAnimator;
        SetupButtons();
        mGUI.SetActive(true);
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
                            mCurrentPhase = Phase.PLAYER_SWITCH;
                            hasAttacked = false;
                            break;


                    }

                    if (hasAttacked)
                        mCurrentPhase = Phase.PLAYER_ATTACK;

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

    void CheckDeath()
    {
        if (mPlayerMonster.GetHP() <= 0)
        {
            mCurrentPhase = Phase.PLAYER_SWITCH;
            playerLosses++;
        }
        if (mEnemyMonster.GetHP() <= 0)
        {
            mCurrentPhase = Phase.ENEMY_SWITCH;
            enemyLosses++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        print(mCurrentPhase);
        switch (mCurrentPhase)
        {
            //Random screen wipe effect like in pokemon here if we have time
            //This will also be where music changes
            case Phase.PRE:
                break;

            //Will do the camera transition here
            case Phase.BEGIN:
                mCurrentPhase = Phase.PLAYER_CHOICE;
                break;

            case Phase.PLAYER_CHOICE:
                if (mPlayerAnim.GetCurrentAnimatorStateInfo(0).IsName("idle") && mEnemyAnim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                {
                    if (!mPlayerAnim.IsInTransition(0) && !mEnemyAnim.IsInTransition(0))
                        UIControl();

                }
                break;

            case Phase.PLAYER_ATTACK:
                mCurrentPhase = Phase.OPPONENT_CHOICE;
                CheckDeath();
                break;


            case Phase.OPPONENT_CHOICE:
                if (mPlayerAnim.GetCurrentAnimatorStateInfo(0).IsName("idle") && mEnemyAnim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                {
                    if (!mPlayerAnim.IsInTransition(0) && !mEnemyAnim.IsInTransition(0))
                        AIControl();
                }
                break;

            case Phase.OPPONENT_ATTACK:
                mCurrentPhase = Phase.PLAYER_CHOICE;
                CheckDeath();
                break;

            case Phase.ENEMY_SWITCH:
                if (enemyLosses >= mEnemy.GetComponent<Party>().monsterList.Count)
                    mCurrentPhase = Phase.ENEMY_DEATH;
                else
                {
                    DespawnMonster(mEnemyMonster);
                    mEnemy.GetComponent<Party>().selectedMonster++;
                    SpawnMonster(mEnemy, enemyTransform);
                    NewMonsterInit();
                    mCurrentPhase = Phase.OPPONENT_CHOICE;
                }
                break;

            case Phase.PLAYER_SWITCH:
                if (playerLosses >= mPlayer.GetComponent<Party>().monsterList.Count)
                    mCurrentPhase = Phase.PLAYER_DEATH;
                else
                {
                    DespawnMonster(mPlayerMonster);
                    mPlayer.GetComponent<Party>().selectedMonster++;
                    SpawnMonster(mPlayer, playerTransform);
                    NewMonsterInit();
                    mCurrentPhase = Phase.PLAYER_CHOICE;
                }
                break;

            case Phase.PLAYER_DEATH:
                EndBattle(false);
                break;

            case Phase.ENEMY_DEATH:
                EndBattle(true);
                break;

            

        }

        
        
        
    }


    void EndBattle(bool playerWon)
    {
        GamePersistantData.Instance.playerWon = playerWon;
        mGUI.SetActive(false);
        mEnemy.GetComponent<Party>().isDefeated = playerWon;
        GameObject.FindGameObjectWithTag("Enemy").GetComponent<DialogueHandler>().OpenPrompt(playerWon ? "battle_lost" : "battle_won");
        CameraSwitcher.UseOverworldCamera();
        DespawnMonster(mPlayerMonster);
        DespawnMonster(mEnemyMonster);
        mCurrentPhase = Phase.PRE;
        GamePersistantData.Instance.mAudio.clip = GamePersistantData.Instance.worldClip;
        GamePersistantData.Instance.mAudio.Play();
    }
}
