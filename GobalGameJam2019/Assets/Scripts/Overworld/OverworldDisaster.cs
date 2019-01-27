using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldDisaster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print(GameObject.FindGameObjectsWithTag("Player").Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
