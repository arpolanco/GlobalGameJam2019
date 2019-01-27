using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using System.Linq;
using UnityEngine.SceneManagement;

public class DialogueHandler : MonoBehaviour
{
    //Editor Variables
    public string dialogueFilename;

    //Local variables
    private IEnumerable<XElement> prompts;
    private XElement currentPrompt;
    private bool inDialogue = false;

    // Verify and load file
    void Start()
    {
        string verifyResult = DialogueVerifier.Verify(dialogueFilename);
        if (verifyResult != "")
        {
            print("Dialogue Error:\n " + verifyResult);
            Debug.Assert(1 == 0);
        }
        XElement tree = XElement.Load(dialogueFilename);
        prompts =
            from n in tree.Descendants()
            where n.Name == "prompt"
            select n;
        currentPrompt = prompts.Where(x => x.Attribute("id").Value == "root").First();
    }


    void OnGUI()
    {
        if (inDialogue)
        {
            int x = Screen.width / 10;
            int y = Screen.height / 10;
            int w = Screen.width * 8 / 10;
            int h = Screen.height / 10;
            GUI.TextArea(new Rect(x, y, w, h), currentPrompt.Attribute("text").Value);
            int i = 0;
            foreach (XElement option in currentPrompt.Descendants())
            {
                if (GUI.Button(new Rect(x, y * (4 + i), w, h), option.Value))
                {
                    switch (option.Attribute("action").Value)
                    {
                        case "prompt":
                            currentPrompt = prompts.Where(foo => foo.Attribute("id").Value == option.Attribute("prompt").Value).First();
                            break;
                        case "exit":
                            inDialogue = false;
                            break;
                        case "battle":
                            CameraSwitcher.UseArenaCamera();
                            GameObject player = GameObject.FindGameObjectWithTag("Player");
                            gameObject.tag = "Enemy";
                            GameObject.Find("BattlePhaseController").GetComponent<BattlePhaseManager>().StartBattle(player, gameObject);
                            //GamePersistantData.Instance.StorePreBattleData();
                            //GamePersistantData.Instance.StoreEnemyData(gameObject);
                            inDialogue = false;
                            GamePersistantData.Instance.mAudio.clip = GamePersistantData.Instance.battleClip;
                            GamePersistantData.Instance.mAudio.Play();
                            //SceneManager.LoadScene("new_arena");
                            break;
                    }
                }
                i++;
            }
        }
    }

    public void StartDialogue()
    {
        inDialogue = true;
    }

    public void OpenPrompt(string s)
    {
        inDialogue = true;
        currentPrompt = prompts.Where(x => x.Attribute("id").Value == s).First();
    }
}
