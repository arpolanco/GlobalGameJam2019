﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPrintEventComp : GameEventComponent
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void CallEvent()
    {
        print("Test Event print");
    }
}
