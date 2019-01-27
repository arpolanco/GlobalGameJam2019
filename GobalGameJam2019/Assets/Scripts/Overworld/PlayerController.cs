using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public enum PlayerState { Walking, Idle };

    PlayerState playerState = new PlayerState();

    Animator playerAnimator;

    private float walkSpeed = 3f;

    private Vector3 currentWalkingDirection = new Vector3(1, 0, 0);

    private GameObject lootboxCanvas, partyUI;

    public int currency = 50;

    // Start is called before the first frame update
    void Start()
    {
        playerState = PlayerState.Idle;

        playerAnimator = GetComponent<Animator>();
        lootboxCanvas = GameObject.Find("LootboxCanvas");
        lootboxCanvas.SetActive(false);
        partyUI = GameObject.Find("PartyUI");
        partyUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraSwitcher.inBattle || CameraSwitcher.inDialogue)
        {
            playerAnimator.Play("HumanoidIdle");
            return;
        }
        if (Input.GetAxis("Forward") != 0)
        {
            playerState = PlayerState.Walking;
            playerAnimator.StopPlayback();
            if (walkSpeed == 3)
                playerAnimator.Play("HumanoidWalk");
            else
                playerAnimator.Play("HumanoidRun");
            currentWalkingDirection = new Vector3(0, 0, Input.GetAxis("Forward"));
        }
        if (Input.GetAxis("Right") != 0)
        {
            playerState = PlayerState.Walking;
            playerAnimator.StopPlayback();
            if (walkSpeed == 3)
                playerAnimator.Play("HumanoidWalk");
            else
                playerAnimator.Play("HumanoidRun");
            currentWalkingDirection = new Vector3(Input.GetAxis("Right"), 0, 0);
        }
        if (Input.GetAxis("Forward") == 0 && Input.GetAxis("Right") == 0)
        {
            playerState = PlayerState.Idle;
            playerAnimator.StopPlayback();
            playerAnimator.Play("HumanoidIdle");
        }

        if (Input.GetButtonDown("Sprint"))
        {
            walkSpeed = 8;
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            walkSpeed = 3;
        }
        if (Input.GetKeyDown(KeyCode.L))
            lootboxCanvas.SetActive(!lootboxCanvas.activeSelf);
        if (Input.GetKeyDown(KeyCode.P))
            partyUI.SetActive(!partyUI.activeSelf);
        if (Input.GetKeyDown(KeyCode.Space))
            AddMonster(null);
        transform.forward = currentWalkingDirection;

        if (playerState == PlayerState.Walking)
        {
            transform.Translate(currentWalkingDirection * walkSpeed * Time.deltaTime, Space.World);
        }


    }

    void AddMonster(Entity e)
    {
        GameObject go = new GameObject();
        go.AddComponent<Entity>();
        go.GetComponent<Entity>().CopyData(e);
        go.transform.parent = transform;
        go.name = e.monsterName.Replace(' ', '_');
        GetComponent<PlayerParty>().AddMonster(e);
        //GetComponent<PlayerParty>().LoadParty();
    }

    public void GetLootboxMonster(LootboxUI ui)
    {
        Entity e = lootboxCanvas.GetComponent<LootboxUI>().GetResult();
        if (e != null)
            AddMonster(e);
    }
}
