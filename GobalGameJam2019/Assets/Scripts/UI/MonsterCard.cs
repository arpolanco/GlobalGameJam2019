using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MonsterCard : MonoBehaviour, IDragHandler, IEndDragHandler 
{
    public Entity monster;
    public Text name;
    public Text type;
    public Text damage;
    public Image icon;

    GameObject[] slots;

    GameObject[] partyslots;

    public bool isSelected = false;

    public ResourceBar resourceBar;

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Transform parent = transform.parent;
        foreach (GameObject go in slots) {
            RectTransform  rect = go.GetComponent<RectTransform>();
            if (RectTransformUtility.RectangleContainsScreenPoint(rect, Input.mousePosition))
            {

                if (go.transform.childCount > 0)
                {
                    Transform othermonster = go.transform.GetChild(0);
                    othermonster.parent = transform.gameObject.transform.parent;
                    othermonster.localPosition = Vector3.zero;
                    othermonster.localScale = Vector3.one;
                }
                transform.parent = go.transform;

            }
        }

        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;


    }

    // Start is called before the first frame update
    void Start()
    {

      
        slots = GameObject.FindGameObjectsWithTag("MonsterSlot");

        partyslots = GameObject.FindGameObjectsWithTag("PartySlot");


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

        damage.text = "Damage: " + monster.damage;

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
