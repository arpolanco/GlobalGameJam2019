using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Party : MonoBehaviour
{
    public List<Entity> monsterList;
    public int selectedMonster = 0;
    public int reward = 25;

    public bool isDefeated = false;     //I know this is a horrible place to put this but it's aleady here...

    public void Awake()
    {
        LoadMonsters();
    }

    public void LoadMonsters()
    {
        monsterList = new List<Entity>();
        foreach(Entity entity in transform.GetComponentsInChildren<Entity>())
        {
            monsterList.Add(entity);
        }
    }

    public Entity GetEntity()
    {
        return monsterList[selectedMonster];
    }
}
