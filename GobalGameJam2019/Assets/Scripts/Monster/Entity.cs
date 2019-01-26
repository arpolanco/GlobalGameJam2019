
using System;
using UnityEngine;

public enum Element { NORMIE = 0, FIRE = 1, WATER = 2, GRASS = 3 }

public class Entity : MonoBehaviour
{
    private static float[,] elementMatrix = GetElementAdjacencyMatrix();

    [SerializeField] private String monsterName { get; }
    [SerializeField] private Element element { get; }
    [SerializeField] private int hp;
    [SerializeField] private int maxHp;
    [SerializeField] private int damage;

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
        gameObject.GetComponent<Animator>().SetTrigger("HIT");

        if(this.hp <= 0)
            gameObject.GetComponent<Animator>().SetTrigger("DIE");
    }

    public int GetMaxHP()
    {
        return maxHp;
    }

    public int GetHP()
    {
        return hp;
    }
}
