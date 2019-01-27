using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParty : MonoBehaviour
{
    public List<Entity> monsterInventory;
    public List<Entity> party;
    // Start is called before the first frame update
    void Start()
    {
        party = new List<Entity>();
        for(int i = 0; i < 3; ++i)
        {
            if (monsterInventory[i] != null)
            {
                party.Add(monsterInventory[i]);
                monsterInventory.RemoveAt(i);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
