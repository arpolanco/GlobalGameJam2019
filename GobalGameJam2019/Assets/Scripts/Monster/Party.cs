using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Party : MonoBehaviour
{
    public List<Entity> monsterList;
    public int selectedMonster = 0;

    public void Awake()
    {
        LoadMonsters();
    }

    private void LoadMonsters()
    {
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
