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

    public int index = -1;
    public int partyindex = -1;

    GameObject[] slots;

    GameObject[] partyslots;

    public PlayerParty playerParty;



    public bool isSelected = false;

    public ResourceBar resourceBar;

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    void SwapPlacesVisually(Transform other)
    {
        Transform newparent = other.parent;
        //swapping visually where they are
        other.parent = transform.gameObject.transform.parent;
        other.localPosition = Vector3.zero;
        other.localScale = Vector3.one;
        transform.parent = newparent;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        Transform parent = transform.parent;
        
        //Inventory
        foreach (GameObject go in slots) {
            RectTransform  rect = go.GetComponent<RectTransform>();
            if (RectTransformUtility.RectangleContainsScreenPoint(rect, Input.mousePosition))
            {
                int oldindex = index;
                int newindex = index;
                if (go.transform.childCount > 0)
                {
                    Transform othermonster = go.transform.GetChild(0);

                    //moving inventory around
                    if (index >= 0)
                    {
                        //actually changing the players party
                        newindex = othermonster.gameObject.GetComponent<MonsterCard>().index;
                        othermonster.gameObject.GetComponent<MonsterCard>().index = oldindex;
                        index = newindex;

                        playerParty.monsterInventory[oldindex] = othermonster.gameObject.GetComponent<MonsterCard>().monster;
                        playerParty.monsterInventory[newindex] = monster;

                    //moving from party to inventory
                    }else if(partyindex >= 0)
                    {
                        playerParty.party[partyindex] = othermonster.gameObject.GetComponent<MonsterCard>().monster;
                        playerParty.monsterInventory[othermonster.gameObject.GetComponent<MonsterCard>().index] = monster;
                    }

                    SwapPlacesVisually(othermonster);



                }
               
                
            }


        }


        foreach (GameObject go in partyslots)
        {
            RectTransform rect = go.GetComponent<RectTransform>();
            if (RectTransformUtility.RectangleContainsScreenPoint(rect, Input.mousePosition))
            {
                int oldindex = index;
                int newindex = index;
                if (go.transform.childCount > 0)
                {
                    Transform othermonster = go.transform.GetChild(0);

                    //moving inventory to party
                    if (index >= 0)
                    {
                        //actually changing the players party
                        newindex = othermonster.gameObject.GetComponent<MonsterCard>().partyindex;
                        othermonster.gameObject.GetComponent<MonsterCard>().index = index;

                        playerParty.monsterInventory[index] = othermonster.gameObject.GetComponent<MonsterCard>().monster;
                        playerParty.party[newindex] = monster;

                    //moving from party to party
                    }
                    else if (partyindex >= 0)
                    {
                       
                        playerParty.party[partyindex] = othermonster.gameObject.GetComponent<MonsterCard>().monster;
                        playerParty.party[othermonster.gameObject.GetComponent<MonsterCard>().partyindex] = monster;
                    }

                    SwapPlacesVisually(othermonster);



                }


            }


        }

        resetPosition();

    }

    void InventoryControl()
    {

    }


    public void resetPosition()
    {
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
    }
    // Start is called before the first frame update
    void Start()
    {
        slots = GameObject.FindGameObjectsWithTag("MonsterSlot");
        partyslots = GameObject.FindGameObjectsWithTag("PartySlot");
    }

    // Update is called once per frame
    void Update()
    {
        resourceBar.SetCurrentResource(monster.GetHP());
       
    }


    public void Initialize(Entity m)
    {
        monster = m;



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
        icon.sprite = monster.icon;
    }


}
