using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customizer : MonoBehaviour
{
    public Skin[] skin;
    public ClothesTypes[] clothesTypes;

    public void NextClother(int SkinnedTypeIndex)
    {
        int nextClothesIndex;
        nextClothesIndex = skin[SkinnedTypeIndex].currentIndex +1;

        if (nextClothesIndex >= clothesTypes[SkinnedTypeIndex].clothes.Length)
        {
            nextClothesIndex = 0;
        }

        AddClothes(clothesTypes[SkinnedTypeIndex].clothes[nextClothesIndex]);
        skin[SkinnedTypeIndex].currentIndex = nextClothesIndex;
    }

    public void PreviousClother(int SkinnedTypeIndex)
    {
        int nextClothesIndex;

        nextClothesIndex = skin[SkinnedTypeIndex].currentIndex -1;
        if (nextClothesIndex < 0)
        {
            nextClothesIndex = clothesTypes[SkinnedTypeIndex].clothes.Length-1;
        }

        AddClothes(clothesTypes[SkinnedTypeIndex].clothes[nextClothesIndex]);
        skin[SkinnedTypeIndex].currentIndex = nextClothesIndex;
    }


    void AddClothes(GameObject newClother)
    {
        int SlotIndex = (int)newClother.GetComponent<Mc_Attachment>().type;

        skin[SlotIndex].skinnedMeshRenderer.sharedMesh = newClother.GetComponent<MeshFilter>().sharedMesh;
        skin[SlotIndex].skinnedMeshRenderer.materials = newClother.GetComponent<MeshRenderer>().sharedMaterials;

    }
    public enum Type
    {
        UpperBody,
        LowerBody,
        Foot,
        Hand,
        Vest,
        BackPack,       
        Hair,
        Head,
        HeadAttachment,
        Mask,
    }

}
[System.Serializable]
public class Skin
{
    public string name;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    [HideInInspector]
    public int currentIndex;
}

[System.Serializable]
public class ClothesTypes
{
    public string name;
    public GameObject[] clothes;
}


