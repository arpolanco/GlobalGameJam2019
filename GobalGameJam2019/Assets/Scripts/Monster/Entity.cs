using System;
using UnityEngine;
using System.Collections.Generic;

public enum Element { NORMIE = 0, FIRE = 1, WATER = 2, GRASS = 3 }


public class Entity : MonoBehaviour
{
    private static float[,] elementMatrix = GetElementAdjacencyMatrix();

    public String monsterName;
    public Element element;
    public List<MoveInfo> moveList = new List<MoveInfo>();
    public UnityEngine.Object prefab;
    public float initalYOffset = 0;
    [SerializeField] private int hp;
    [SerializeField] private int maxHp;
    [SerializeField] public int damage;
    public Animator currentAnimator;

    [SerializeField] public Sprite icon;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private static float[,] GetElementAdjacencyMatrix()
    {
        return new float[,]
        {
            { 1.0f, 1.0f, 1.0f, 1.0f },
            { 1.0f, 1.0f, 0.5f, 2.0f },
            { 1.0f, 2.0f, 1.0f, 0.5f },
            { 1.0f, 0.5f, 2.0f, 1.0f }
        };
    }

    private static int GetElementFactor(Entity a, Entity b)
    {
        int row = Convert.ToInt32(a.element);
        int col = Convert.ToInt32(b.element);
        return Convert.ToInt32(elementMatrix[row, col]);
    }

    public void Attack(Entity target)
    {
        int totalDamage = damage * GetElementFactor(this, target);
        target.TakeDamage(totalDamage);
    }

    public void TakeDamage(int damage)
    {
        this.hp -= damage;
        currentAnimator.SetTrigger("HIT");

        if(this.hp <= 0)
            currentAnimator.SetTrigger("DIE");
    }

    public int GetMaxHP()
    {
        return maxHp;
    }

    public void SetMaxHP(int val)
    {
        maxHp = val;
        hp = maxHp;
    }

    public int GetHP()
    {
        return hp;
    }

    public void CopyData(Entity e)
    {
        monsterName = e.monsterName;
        element = e.element;
        moveList = new List<MoveInfo>();
        prefab = e.prefab;
        initalYOffset = e.initalYOffset;
        hp = e.hp;
        maxHp = e.GetMaxHP();
        damage = e.damage;
        currentAnimator = null;
        foreach (MoveInfo m in e.moveList)
        {
            moveList.Add(m);
        }
        icon = e.icon;
    }
}
