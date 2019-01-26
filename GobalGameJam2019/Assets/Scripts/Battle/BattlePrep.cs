using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePrep : MonoBehaviour
{
    public Transform playerTransform, enemyTransform;
    public GameObject player, enemy;
    public BattlePhaseManager manager;

    // Start is called before the first frame update
    void Start()
    {
        LaunchBattle();
        manager.StartBattle(player, enemy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LaunchBattle()
    {
        SpawnMonster(player, playerTransform); 
        SpawnMonster(enemy, enemyTransform);
    }

    void SpawnMonster(GameObject trainer, Transform transform)
    {
        Party party = trainer.GetComponent<Party>();
        GameObject go = Instantiate(party.GetEntity().prefab, transform.position + party.GetEntity().initalYOffset * Vector3.up, transform.rotation, null) as GameObject;
        party.GetEntity().currentAnimator = go.GetComponent<Animator>();
    }
}
