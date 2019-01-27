using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    //Misc Player Stuff that you dont know where to put it
    public int moneyPouch = 10;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addMoney(int gold)
    {
        moneyPouch += gold;
        if (moneyPouch > 99999)
            moneyPouch = 99999;
    }

    public void subMoney(int gold)
    {
        moneyPouch -= gold;
        if (moneyPouch <= 0)
            moneyPouch = 0;
    }
}
