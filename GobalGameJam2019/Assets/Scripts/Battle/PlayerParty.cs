using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParty : MonoBehaviour
{
    public List<Entity> monsterInventory = new List<Entity>();
    public List<Entity> party = new List<Entity>();
    public int selectedIndex = 0;

    public Entity test;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddMonster(Entity e)
    {
        if(party.Count < 3)
        {
            party.Add(e);
        }
        else
        {
            monsterInventory.Add(e);
        }
    }

    public Entity GetEntity()
    {
        return party[selectedIndex];
    }
}
