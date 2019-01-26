using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Party : MonoBehaviour
{
    public List<Entity> monsterList;

    public void Start()
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
}
