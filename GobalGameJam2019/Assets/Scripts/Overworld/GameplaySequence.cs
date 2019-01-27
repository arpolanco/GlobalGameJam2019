using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySequence : Singleton<GameplaySequence>
{

    private int eventPosition = 0;


    [SerializeField]
    GameObject trainer0;

    [SerializeField]
    GameObject trainer1;


    void Update()
    {
        switch (eventPosition)
        {
            case 0:
                //if (trainer0.GetComponent<NPCOverworldComp>().isDefeated && trainer1.GetComponent<NPCOverworldComp>().isDefeated)
                //    IncreaseEventPosition();
                break;
            case 1:
                break;
        }
    }


    public void IncreaseEventPosition(int amt = 1)
    {
        eventPosition += amt;
    }
}
