using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActivatorComponent : MonoBehaviour
{
    [SerializeField]
    private bool isButtonTrigger = false;

    private BoxCollider boxTrigger;

    private bool isPlayerInTrigger = false;
    
    private GameEventComponent gameEventComp;


    // TODO: Won't need this later. Keeping it to retain logic. Replace with Input later
    private bool hasHitButton = false;


    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<BoxCollider>() == null)
            isPlayerInTrigger = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInTrigger)
        {
            if (!isButtonTrigger || (isButtonTrigger && hasHitButton))
            {
                GetComponent<GameEventComponent>().CallEvent();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }


    private void OnTriggerExit(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }
}
