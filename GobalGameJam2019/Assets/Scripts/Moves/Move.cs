using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveClassification {DAMAGE, STATUS, BUFF}

[System.Serializable]
public class MoveInfo
{
    public Move move;
    public AnimType animType;
    public ParticleType particleType;
}

[System.Serializable]
public struct MoveEffect
{
    public Element element;
    public MoveClassification classification;
    public float quantVal;
    public string qualVal;
}

[System.Serializable]
public class Move : ScriptableObject
{
    public string id, particleEffect;
    public List<MoveEffect> effects;
}
