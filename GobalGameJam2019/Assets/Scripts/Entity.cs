
using UnityEngine;

/*
physical dmg
magic dmg
HP
MP

playAttackAnimation()
*/

public class Entity : MonoBehaviour
{
    private int hp, mp;

    public int maxHp, maxMp;
    public int physicalDamage;
    public int magicDamage;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        mp = maxMp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playAnimation()
    {
        // TODO
    }
}
