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

    // Start is called before the first frame update
    void Start()
    {
        playerState = PlayerState.Idle;

        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Forward") != 0)
        {
            playerState = PlayerState.Walking;
            playerAnimator.StopPlayback();
            playerAnimator.Play("HumanoidWalk");
            currentWalkingDirection = new Vector3(0, 0, Input.GetAxis("Forward"));
        }
        if (Input.GetAxis("Right") != 0)
        {
            playerState = PlayerState.Walking;
            playerAnimator.StopPlayback();
            playerAnimator.Play("HumanoidWalk");
            currentWalkingDirection = new Vector3(Input.GetAxis("Right"), 0, 0);
        }
        if (Input.GetAxis("Forward") == 0 && Input.GetAxis("Right") == 0)
        {
            playerState = PlayerState.Idle;
            playerAnimator.StopPlayback();
            playerAnimator.Play("HumanoidIdle");
        }

        transform.forward = currentWalkingDirection;

        if (playerState == PlayerState.Walking)
        {
           transform.Translate(currentWalkingDirection * walkSpeed * Time.deltaTime, Space.World);
        }

        
    }
}
