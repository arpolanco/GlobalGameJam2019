using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartDialogueEventComp : GameEventComponent
{
    public GameObject npc;
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
        npc.GetComponent<DialogueHandler>().StartDialogue();
    }
}