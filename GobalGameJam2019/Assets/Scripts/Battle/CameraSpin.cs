using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpin : MonoBehaviour
{
    public float spinFactor = 1;
    
    void Update()
    {
        transform.Rotate(new Vector3(0, 1, 0), Time.deltaTime * spinFactor);
    }
}
