using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class PartyUI : MonoBehaviour
{

    GameObject[] slots;
    GameObject[] partyslots;

    Party party;
    
    // Start is called before the first frame update
    void Start()
    {
        slots = GameObject.FindGameObjectsWithTag("MonsterSlot");
        partyslots = GameObject.FindGameObjectsWithTag("PartySlot");

    }

    // Update is called once per frame
    void Update()
    {
      
    }

 
}

