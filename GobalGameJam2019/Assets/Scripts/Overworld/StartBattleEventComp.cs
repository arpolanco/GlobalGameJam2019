using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBattleEventComp : GameEventComponent
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
        base.CallEvent();

        print("DENELA-DENELA-DENELA-DENELA-DENELA-DENELA-DENELA-DENELA");
        print("DE. DE. DEH DEH-DEH-DEH!");
        print("Deh neehhhhh, De-ne-ne-nehhhhhhhhh");
        print("dooooOooo, DOOOOOo Doooooo dooooooo");

        SceneManager.LoadScene("new_arena");
    }
}
