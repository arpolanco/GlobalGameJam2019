using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveMaker
{
    [MenuItem("Assets/Create/Move")]
    public static void CreateMyAsset()
    {
        Move asset = ScriptableObject.CreateInstance<Move>();

        AssetDatabase.CreateAsset(asset, "Assets/Files/Moves/tmp.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}
