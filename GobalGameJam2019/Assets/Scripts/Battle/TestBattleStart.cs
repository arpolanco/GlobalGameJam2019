using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBattleStart : MonoBehaviour
{
    bool done = false;
    void OnGUI()
    {
        if(!done && GUI.Button(new Rect(100, 100, 100, 100), "BATTLE!")){
            BattlePhaseManager manager = GameObject.Find("BattlePhaseController").GetComponent<BattlePhaseManager>();
            manager.StartBattle(GameObject.Find("Player"), GameObject.Find("Enemy"));
            done = true;
        }
    }
}
