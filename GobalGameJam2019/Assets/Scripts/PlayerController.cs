using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        //playerAnimator.Play("HumanoidWalk");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
