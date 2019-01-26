using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOverworldCamera : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    List<GameObject> topFloorList = new List<GameObject>();
    
    [SerializeField]
    List<GameObject> midFloorList = new List<GameObject>();

    [SerializeField]
    GameObject topFloorStairs;

    private int currentFloor = 2;

    float targetStairAlpha = 1;

    bool shouldUpdateStairs = false;

    float stairLerp = 0;

    
    void Update()
    {
        // Update camera position
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + 13.9f, playerTransform.position.z - 4.63f);

        CheckFloorLevel();

        if (shouldUpdateStairs)
            UpdateStairs(topFloorStairs);
    }

    // This whole system is a horrible mess...

    private void CheckFloorLevel()
    {
        if (currentFloor == 2 && playerTransform.localPosition.y < 6.3f)
        {
            HideFloors(topFloorList, false, topFloorStairs, .05f);
            currentFloor = 1;
        }
        else if (currentFloor == 1 && playerTransform.localPosition.y > 6.38f)
        {
            HideFloors(topFloorList, true, topFloorStairs, 1f);
            currentFloor = 2;
        }
    }

    
    private void HideFloors(List<GameObject> floorList, bool floorsEnabled, GameObject stairs, float stairAlpha)
    {
        for (int i = 0; i < floorList.Count; i++)
        {
            floorList[i].SetActive(floorsEnabled);
        }
        targetStairAlpha = stairAlpha;

        shouldUpdateStairs = true;
    }

    private void UpdateStairs(GameObject stairs)
    {
        Material stairMat = stairs.GetComponent<MeshRenderer>().material;
        Color stairColor = stairMat.color;


        stairLerp = Time.deltaTime * 2f;

        if (stairColor.a != targetStairAlpha)
        {
            stairColor.a = Mathf.Lerp(stairColor.a, targetStairAlpha, stairLerp);
            stairMat.color = stairColor;
            stairs.GetComponent<MeshRenderer>().material = stairMat;
        }
        else
        {
            stairLerp = targetStairAlpha;
            shouldUpdateStairs = false;
        }
            
        
        
    }
}
