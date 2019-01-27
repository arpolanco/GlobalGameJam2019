using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class PartyUI : MonoBehaviour
{

    public GameObject[] slots;
    public GameObject[] partyslots;

    public PlayerParty playerParty;
    public GameObject monsterCard;

    public Text pagetext;

    private int offset = 0;
    // Start is called before the first frame update
    void Start()
    {
        slots = GameObject.FindGameObjectsWithTag("MonsterSlot");
        partyslots = GameObject.FindGameObjectsWithTag("PartySlot");

        for(int i = 0; i < 3; ++i)
        {
            if(playerParty.party[i] != null)
            {
                GameObject card = Instantiate(monsterCard);
                card.transform.parent = partyslots[i].transform;
                card.GetComponent<MonsterCard>().Initialize(playerParty.party[i]);
                card.GetComponent<MonsterCard>().resetPosition();
                card.GetComponent<MonsterCard>().playerParty = playerParty;
            }
        }


        for (int i = 0; i < 6; ++i)
        {
            if (playerParty.monsterInventory[i] != null)
            {
                GameObject card = Instantiate(monsterCard);
                card.transform.parent = slots[i].transform;
                card.GetComponent<MonsterCard>().Initialize(playerParty.monsterInventory[i]);
                card.GetComponent<MonsterCard>().resetPosition();
                card.GetComponent<MonsterCard>().playerParty = playerParty;
            }
        }

        UpdatePartyStatus();
    }

    // Update is called once per frame
    void Update()
    {
        pagetext.text = "Page: " + (offset + 1);
        UpdatePartyStatus();
    }
    public void ClearPage()
    {
        for (int i = 0; i < 6; ++i)
        {
            if (slots[i].transform.childCount> 0)
            {
                Destroy(slots[i].transform.GetChild(0).gameObject);
            }
        }
    }

    public void UpdatePage()
    {
        ClearPage();

        for (int i = 0; i < 6; ++i)
        {
       
            if((i + (offset * 6)) < playerParty.monsterInventory.Count)
            {
                GameObject card = Instantiate(monsterCard);
                card.transform.parent = slots[i].transform;
                card.GetComponent<MonsterCard>().Initialize(playerParty.monsterInventory[i+ offset * 6]);
                card.GetComponent<MonsterCard>().resetPosition();
                card.GetComponent<MonsterCard>().playerParty = playerParty;
            }
            else
            {
                break;
            }

        }

        UpdatePartyStatus();
    }
    void UpdatePartyStatus()
    {
        int i = 0;
        foreach (GameObject slot in slots)
        {
            if(slot.transform.childCount> 0)
            {
                MonsterCard card = slot.transform.GetChild(0).GetComponent<MonsterCard>();
                Entity mon = card.monster;

                card.partyindex = -1;
                card.index = i + offset * 6;

            }
            ++i;
        }

       i = 0;
        foreach (GameObject slot in partyslots)
        {
            if (slot.transform.childCount > 0)
            {
                MonsterCard card = slot.transform.GetChild(0).GetComponent<MonsterCard>();
                Entity mon = card.monster;

                card.partyindex = i;
                card.index = -1;
                

            }
            ++i;
        }
    }
    public void ChangePage(int p)
    {

        if (((offset+1) * 6 > playerParty.monsterInventory.Count) && (p > 0))
        {

        }
        else
        {
            offset += p;
        }
        
        if (offset < 0)
            offset = 0;

        
        UpdatePage();
        

    }

 
}

