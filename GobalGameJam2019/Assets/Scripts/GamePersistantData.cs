using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePersistantData : Singleton<GamePersistantData>
{
    [SerializeField]
    GameObject playerObject;

    [SerializeField]
    public Party playerParty;       // this doesn't 

    private Transform playerTransformPreBattle;

    [SerializeField]
    public List<GameObject> worldNPCObjects = new List<GameObject>();

    private List<Transform> worldNPCStoredTransforms = new List<Transform>();



    public void StorePreBattleData()
    {
        for (int i = 0; i < worldNPCObjects.Count; ++i)
        {
            worldNPCStoredTransforms[i] = worldNPCObjects[i].transform;
        }

        playerTransformPreBattle = playerObject.transform;
    }

    public void SetPostBattleWorldData()
    {
        for (int i = 0; i < worldNPCObjects.Count; ++i)
        {
            worldNPCObjects[i].transform.SetPositionAndRotation(worldNPCStoredTransforms[i].position, worldNPCStoredTransforms[i].rotation);
        }

        playerObject.transform.SetPositionAndRotation(playerTransformPreBattle.position, playerTransformPreBattle.rotation);
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
