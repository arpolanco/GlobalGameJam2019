using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveClassification {DAMAGE, STATUS, BUFF}

[System.Serializable]
public struct MoveEffect
{
    public Element element;
    public MoveClassification classification;
    public float quantVal;
    public string qualVal;
}


public class Move : ScriptableObject
{
    public string id, animation, particleEffect;
    public List<MoveEffect> effects;
}
