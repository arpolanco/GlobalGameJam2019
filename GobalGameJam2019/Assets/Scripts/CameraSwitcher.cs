using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraSwitcher
{
    public static void UseOverworldCamera() {
        GameObject.Find("OverworldCamera").GetComponent<Camera>().enabled = true;
        GameObject.Find("ArenaCamera").GetComponent<Camera>().enabled = false;
        GameObject.Find("ArenaCamera").GetComponentInChildren<Light>().enabled = false;
    }

    public static void UseArenaCamera()
    {
        GameObject.Find("OverworldCamera").GetComponent<Camera>().enabled = false;
        GameObject.Find("ArenaCamera").GetComponent<Camera>().enabled = true;
        GameObject.Find("ArenaCamera").GetComponentInChildren<Light>().enabled = true;


    }
}
