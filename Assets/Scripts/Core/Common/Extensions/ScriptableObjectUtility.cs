using System;
using System.IO;
using UnityCore.Extensions;
using UnityEngine;
using UnityEditor;


public static class ScriptableObjectUtility
{
    /// <summary>
    //	This makes it easy to create, name and place unique new ScriptableObject asset files.
    /// </summary>
    public static T CreateAsset<T>(string savePath = null, string fileName = null, Action<T> beforeSave = null) where T : ScriptableObject
    {
        T asset = ScriptableObject.CreateInstance<T>();
#if UNITY_EDITOR
        string path = savePath ?? AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path == string.Empty)
        {
            path = "Assets";
        }
        else if (Path.GetExtension(path) != "")
        {
            path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
        }

        if (string.IsNullOrEmpty(fileName))
            fileName = "/New " + typeof(T);
        else
            fileName = "/" + fileName;
        
        Debug.Log("saving:" + path + fileName + ".asset");
        var assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + fileName + ".asset");
        beforeSave.SafeRaise(asset);
        AssetDatabase.CreateAsset(asset, assetPathAndName);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
#endif
        return asset;
    }
}