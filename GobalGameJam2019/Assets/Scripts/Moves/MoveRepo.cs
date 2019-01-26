using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using System.Linq;

public class MoveRepo : MonoBehaviour
{
    //Editor variables
    public string moveListFilename;

    //Local variables
    private Dictionary<string, Move> moveMap;
    private IEnumerable<XElement> moveList;

    void Start()
    {
        LoadXML();
    }

    void LoadXML()
    {
        XElement tree = XElement.Load(moveListFilename);
        moveList =
            from n in tree.Descendants()
            where n.Name == "move"
            select n;
        foreach(XElement move in moveList)
        {
            ProcessMove(move);
        }
    }
    
    void ProcessMove(XElement move)
    {
        if (move.Descendants().Where(x => x.Name == "id") == null)
        {
            print("Move List Error: Move without an id.");
            Debug.Break();
        }
        string id = move.Descendants().Where(x => x.Name == "id").First().Value;
        if (move.Descendants().Where(x => x.Name == "effect") == null)
        {
            print("Move List Error: Move " + id + " does not have any effects. All moves must have at least one effect.");
            Debug.Break();
        }
        List<MoveEffect> effectL = new List<MoveEffect>();
        foreach(XElement effect in move.Descendants().Where(x=>x.Name == "effect"))
        {
            MoveEffect tmpEffect;
            if(move.Descendants().Where(x=>x.Name == "class" || x.Name == "classification").Count() <= 0)
            {
                print("Move \"" + id + "\" does not have a classification. This must be specified.");
                Debug.Break();
            }
            string classString = move.Descendants().Where(x => x.Name == "class" || x.Name == "classification").First().Value;
            tmpEffect.classification = GetClassificationFromString(classString);
            if(tmpEffect.classification == MoveClassification.DAMAGE)
            {

            }

        }
    }

    MoveClassification GetClassificationFromString(string s)
    {
        switch (s.ToUpper())
        {
            case "DAMAGE":
                return MoveClassification.DAMAGE;
            case "STATUS":
                return MoveClassification.STATUS;
            case "BUFF":
                return MoveClassification.BUFF;
            default:
                print("Move Error: Wrong classification. Classification must be damage, status, or buff.");
                Debug.Break();
                return MoveClassification.DAMAGE;
        }
    }

    Move GetMove(string id)
    {
        return moveMap[id];
    }
}
