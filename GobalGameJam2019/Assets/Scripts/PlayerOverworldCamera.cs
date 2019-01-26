using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOverworldCamera : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    Material topFloorMaterial;
    
    [SerializeField]
    Material midFloorMaterial;

    [SerializeField]
    Material topFloorStairsMaterial;

    [SerializeField]
    List<GameObject> topFloorObjects = new List<GameObject>();

    private int currentFloor = 2;

    float targetStairAlpha = 1;

    float targetFloorAlpha = 1;

    bool shouldUpdateStairs = false;

    float stairLerp = 0;

    float floorLerp = 0;

    
    void Update()
    {
        // Update camera position
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + 13.9f, playerTransform.position.z - 4.63f);

        CheckFloorLevel();

        UpdateMats();
    }

    // This whole system is a horrible mess...

    private void CheckFloorLevel()
    {
        if (currentFloor == 2 && playerTransform.localPosition.y < 6.3f)
        {
            HideFloors(topFloorMaterial, topFloorObjects, false, topFloorStairsMaterial, 0, .25f);
            currentFloor = 1;
        }
        else if (currentFloor == 1 && playerTransform.localPosition.y > 6.38f)
        {
            HideFloors(topFloorMaterial, topFloorObjects, true, topFloorStairsMaterial, 1, 1);
            currentFloor = 2;
        }
    }

    
    private void HideFloors(Material floorMat, List<GameObject> floorObjects, bool floorObjectStatus, Material stairs, float floorAlpha, float stairAlpha)
    {

        targetStairAlpha = stairAlpha;
        targetFloorAlpha = floorAlpha;

        shouldUpdateStairs = true;

        for (int i = 0; i < floorObjects.Count; ++i)
        {
            floorObjects[i].SetActive(floorObjectStatus);
        }
    }

    private void UpdateMats()
    {
        Material stairMat = topFloorStairsMaterial; 
        Color stairColor = stairMat.color;
        stairLerp = Time.deltaTime * 2f;
        if (stairColor.a != targetStairAlpha)
        {
            stairColor.a = Mathf.Lerp(stairColor.a, targetStairAlpha, stairLerp);
            stairMat.color = stairColor;
        }
        else
        {
            stairLerp = targetStairAlpha;
        }

        Material floorMat = topFloorMaterial;
        Color floorColor = floorMat.color;
        floorLerp = Time.deltaTime * 2f;
        if (floorColor.a != targetFloorAlpha)
        {
            floorColor.a = Mathf.Lerp(floorColor.a, targetFloorAlpha, floorLerp);
            floorMat.color = floorColor;
        }
        else
        {
            floorLerp = targetFloorAlpha;
        }

        if (floorColor.a == targetFloorAlpha && stairColor.a == targetStairAlpha)
        {
            shouldUpdateStairs = false;
        }

    }


    private void OnDestroy()
    {
        Material floorMat = topFloorMaterial;
        Color floorColor = floorMat.color;
        floorColor.a = 1;
        floorMat.color = floorColor;

        Material stairMat = topFloorStairsMaterial;
        Color stairColor = stairMat.color;
        stairColor.a = 1;
        stairMat.color = stairColor;
    }
}
