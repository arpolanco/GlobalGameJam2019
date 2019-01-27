using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterCard : MonoBehaviour
{
    public Entity monster;
    public Text name;
    public Text type;
    public Text damage;

    public Image icon;

    public ResourceBar resourceBar;
    // Start is called before the first frame update
    void Start()
    {
        name.text = monster.monsterName;
        switch (monster.element)
        {
            case Element.FIRE:
                type.text = "Type: Fire";
                break;

            case Element.WATER:
                type.text = "Type: Water";
                break;

            case Element.GRASS:
                type.text = "Type: Grass";
                break;

            case Element.NORMIE:
                type.text = "Type: Normie";
                break;
        }

        damage.text = "Damange:" + monster.damage;

        resourceBar.SetMaxResource(monster.GetMaxHP());
        resourceBar.SetCurrentResource(monster.GetHP());
        //do icon here
    }

    // Update is called once per frame
    void Update()
    {
        resourceBar.SetCurrentResource(monster.GetHP());
    }
}
