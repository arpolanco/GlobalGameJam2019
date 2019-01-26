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
        gameObject.GetComponentInChildren<Text>().text = monster.name;
        mResourceBar.SetMaxResource(monster.GetMaxHP());

    }
    // Update is called once per frame
    void Update()
    {
        mResourceBar.SetCurrentResource(monster.GetHP());
        
    }
}
