using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraSwitcher
{
    public static bool inBattle = false;
    public static bool inDialogue = false;

    public static void UseOverworldCamera() {
        GameObject.Find("OverworldCamera").GetComponent<Camera>().enabled = true;
        GameObject.Find("ArenaCamera").GetComponent<Camera>().enabled = false;
        GameObject.Find("ArenaCamera").GetComponentInChildren<Light>().enabled = false;
        inBattle = false;
        inDialogue = true;
    }

    public static void UseArenaCamera()
    {
        GameObject.Find("OverworldCamera").GetComponent<Camera>().enabled = false;
        GameObject.Find("ArenaCamera").GetComponent<Camera>().enabled = true;
        GameObject.Find("ArenaCamera").GetComponentInChildren<Light>().enabled = true;
        inBattle = true;
        inDialogue = false;
    }
}
