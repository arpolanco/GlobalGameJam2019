using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LootboxUI : MonoBehaviour
{
    private readonly object AssetDatabse;
    [UnityEngine.SerializeField] private UnityEngine.UI.Text text, hpText, damageText, buttonText;
    [UnityEngine.SerializeField] private UnityEngine.UI.Button button;
    private bool isRolling = false;
    private List<Entity> entities = new List<Entity>();
    private float timer = 0;
    private float timerMax = 0;
    private float lastMilestone = 0;
    private int index = 0;
    private Entity result;
    private GameObject go;
    private GameObject player;

    
    void Start()
    {
        hpText.enabled = false;
        damageText.enabled = false;
        buttonText = button.gameObject.GetComponentInChildren<UnityEngine.UI.Text>();
        buttonText.text = "Roll";
        text.text = "50 coins per roll!";
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isRolling)
        {
            if(timer <= 0)
            {
                timer = 0;
                timerMax = 0;
                isRolling = false;
                go = new GameObject();
                go.AddComponent<Entity>();
                result = go.GetComponent<Entity>();
                result.CopyData(entities[index]);
                int baseHP = result.GetMaxHP();
                int newHP = Mathf.CeilToInt(baseHP * Random.Range(0.75f, 1.25f));
                int diffHP = newHP - baseHP;
                int baseDmg = result.damage;
                int newDmg = Mathf.CeilToInt(baseDmg * Random.Range(0.75f, 1.25f));
                int diffDmg = newDmg - baseDmg;
                result.SetMaxHP(newHP);
                result.damage = newDmg;
                hpText.enabled = true;
                hpText.text = "HP: " + newHP.ToString() + " (" + ((diffHP > 0) ? "+" : "") + diffHP.ToString() + ")";
                damageText.enabled = true;
                damageText.text = "DMG: " + newDmg.ToString() + " (" + ((diffDmg > 0) ? "+" : "") + diffDmg.ToString() + ")";
                buttonText.text = "Claim";
            }
            else
            {
                timer -= Time.deltaTime;
                if (lastMilestone - timer > (timerMax - lastMilestone) / timerMax / 10)
                {
                    Increment();
                }
            }
        }
    }
    private void Increment()
    {
        lastMilestone = timer;
        index = (index + 1) % entities.Count ;
        text.text = entities[index].monsterName;
    }
    public void Roll()
    {
        if (buttonText.text == "Roll")
        {
            if (player.GetComponent<PlayerController>().currency >= 50)
            {
                player.GetComponent<PlayerController>().currency -= 50;
                hpText.enabled = false;
                damageText.enabled = false;
                if (go != null)
                    Destroy(go);
                result = null;
                string[] arr = AssetDatabase.FindAssets("t:Object", new string[] { "Assets/Prefabs/Entities" });
                entities = new List<Entity>();
                foreach (string s in arr)
                {
                    Entity e = (Entity)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(s), typeof(Entity));
                    entities.Add(e);
                    text.text = e.monsterName;
                }
                isRolling = true;
                timer = Random.Range(4, 6);
                timerMax = timer;
                lastMilestone = timerMax;
                index = Mathf.FloorToInt(Random.Range(0, entities.Count));
            }
            else
            {
                text.text = "You're broke, kid.";
            }
        }
        else if (buttonText.text == "Claim")
        {
            player.GetComponent<PlayerController>().GetLootboxMonster(this);
            buttonText.text = "Roll";
            damageText.enabled = false;
            hpText.enabled = false;
            text.text = "50 coins per roll!";
        }
    }

    public Entity GetResult()
    {
        return result;
    }
}
