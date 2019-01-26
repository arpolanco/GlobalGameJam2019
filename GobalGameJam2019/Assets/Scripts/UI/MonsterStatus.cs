using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MonsterStatus : MonoBehaviour
{

    [SerializeField]
    ResourceBar mResourceBar;

    private Entity monster;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void SetMonster(Entity Monster)
    {
        monster = Monster.GetComponent<Entity>();
        gameObject.GetComponentInChildren<Text>().text = monster.monsterName;
        mResourceBar.SetMaxResource(monster.GetMaxHP());

    }
    // Update is called once per frame
    void Update()
    {
        if(monster != null)
            mResourceBar.SetCurrentResource(monster.GetHP());
        
    }
}
