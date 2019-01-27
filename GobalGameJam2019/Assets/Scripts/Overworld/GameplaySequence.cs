using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySequence : Singleton<GameplaySequence>
{

    private int eventPosition = 0;

    [SerializeField]
    private GameObject NPCManager;

    [SerializeField]
    private GameObject Barriers;

    private List<GameObject> currentNPCList = new List<GameObject>();

    void Update()
    {
        switch (eventPosition)
        {
            case 0:
                //dialogue goes here
                //when dialogue is done:
                IncreaseEventPosition();
                break;
            case 1:
                foreach (Transform child in NPCManager.transform.GetChild(0))
                {
                    currentNPCList.Add(child.gameObject);
                }
                IncreaseEventPosition();    //remove later
                break;
            case 2:

                if (AreTrainersDefeated())
                {
                    RemoveBarrier(0);
                    currentNPCList.Clear();
                    foreach (Transform child in NPCManager.transform.GetChild(1))
                    {
                        currentNPCList.Add(child.gameObject);
                    }
                    print("Go to next room!");
                    IncreaseEventPosition();
                }
                break;
            case 3:
                if (AreTrainersDefeated())
                {
                    print("trainer in room two defeated");
                    RemoveBarrier(0);
                    IncreaseEventPosition();
                }
                break;
            case 4:
                print("All done");
                break;
        }
    }

    // You'll almost always want to remove barrier at index 0, assuming they're placed in sequential order
    private void RemoveBarrier(int barrierIndex)
    {
        Destroy(Barriers.transform.GetChild(barrierIndex).gameObject);
    }

    private bool AreTrainersDefeated()
    {
        bool allDefeated = true;
        for (int i = 0; i < currentNPCList.Count; ++i)
        {
            if (!currentNPCList[i].GetComponent<Party>().isDefeated)     //This may not work
                allDefeated = false;
        }
        return allDefeated;
    }


    public void IncreaseEventPosition(int amt = 1)
    {
        eventPosition += amt;
    }
}
