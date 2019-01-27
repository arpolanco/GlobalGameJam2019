using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePersistantData : Singleton<GamePersistantData>
{
    [SerializeField]
    public GameObject playerObject;

    [SerializeField]
    public GameObject enemyObject;

    private Transform playerTransformPreBattle;

    [SerializeField]
    public List<GameObject> worldNPCObjects = new List<GameObject>();

    private List<Transform> worldNPCStoredTransforms = new List<Transform>();

    public bool playerWon = false;

    [HideInInspector]
    public AudioSource mAudio;


    public AudioClip battleClip;

    public AudioClip worldClip;

    private void Awake()
    {
    }
    private void Start()
    {
        mAudio = GetComponent<AudioSource>();
    }
    public void StorePreBattleData()
    {
        for (int i = 0; i < worldNPCObjects.Count; ++i)
        {
            worldNPCStoredTransforms[i] = worldNPCObjects[i].transform;
        }
        playerTransformPreBattle = playerObject.transform;
        playerObject.transform.parent = null;
        DontDestroyOnLoad(playerObject);
        playerObject.SetActive(false);
    }

    public void StoreEnemyData(GameObject enemy)
    {
        enemyObject = enemy;
        DontDestroyOnLoad(enemyObject);
        enemyObject.SetActive(false);
    }

    public void SetPostBattleWorldData()
    {
        for (int i = 0; i < worldNPCObjects.Count; ++i)
        {
            worldNPCObjects[i].transform.SetPositionAndRotation(worldNPCStoredTransforms[i].position, worldNPCStoredTransforms[i].rotation);
        }

        playerObject.transform.SetPositionAndRotation(playerTransformPreBattle.position, playerTransformPreBattle.rotation);
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
