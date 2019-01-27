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
        
        GamePersistantData.Instance.StorePreBattleData();

        SceneManager.LoadScene("new_arena");
    }
}
