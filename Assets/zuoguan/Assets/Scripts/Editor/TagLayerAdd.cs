using UnityEditor;
using UnityEngine;


/// <summary>
/// 自动添加tag和layer
/// </summary>
public class TagLayerAdd : AssetPostprocessor
{
    private static string[] _tagArr = {};
    private static string[] _layerArr = {"Ground", "Invincible", "Player" };
    private static string[] _sortArr = { };
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets,
        string[] movedFromAssetPaths)
    {
        foreach (string s in importedAssets)
        {
            if (s.Equals("Assets/Scripts/Editor/TagLayerAdd.cs"))
            {
                
                Debug.Log("导入项目tag,layer,sortlayer");
                foreach (var tag in _tagArr)
                {
                    AddTag(tag);
                }

                foreach (var layer in _layerArr)
                {
                    AddLayer(layer);
                }

                foreach (var sortlayer in _sortArr)
                {
                    AddSortingLayer(sortlayer);
                }

                return;
            }
        }
    }

    static void AddTag(string tag)
    {
        if (!IsHasTag(tag))
        {
            //
            SerializedObject tagManager =
                new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            SerializedProperty it = tagManager.GetIterator();
            while (it.NextVisible(true))
            {
                if (it.name == "tags")
                {
                    it.InsertArrayElementAtIndex(it.arraySize);
                    SerializedProperty dataPoint = it.GetArrayElementAtIndex(it.arraySize - 1);
                    dataPoint.stringValue = tag;
                    tagManager.ApplyModifiedProperties();
                    return;
                }
            }
        }
    }

    static void AddLayer(string layer)
    {
        if (!IsHasLayer(layer))
        {
            SerializedObject tagManager =
                new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            SerializedProperty it = tagManager.GetIterator();
            while (it.NextVisible(true))
            {
                if (it.name == "layers")
                {
                    for (int i = 0; i < it.arraySize; i++)
                    {
                        if (i == 3) continue;
                        SerializedProperty dataPoint = it.GetArrayElementAtIndex(i);
                        if (string.IsNullOrEmpty(dataPoint.stringValue))
                        {
                            dataPoint.stringValue = layer;
                            tagManager.ApplyModifiedProperties();
                            return;
                        }
                    }
                }
            }
        }
    }

    static void AddSortingLayer(string sortingLayer)
    {
        if (!isHasSortingLayer(sortingLayer))
        {
            SerializedObject tagManager =
                new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            SerializedProperty it = tagManager.GetIterator();
            while (it.NextVisible(true))
            {
                if (it.name == "m_SortingLayers")
                {
                    it.InsertArrayElementAtIndex(it.arraySize);
                    SerializedProperty dataPoint = it.GetArrayElementAtIndex(it.arraySize - 1);
                    while (dataPoint.NextVisible(true))
                    {
                        if (dataPoint.name == "name")
                        {
                            dataPoint.stringValue = sortingLayer;
                            tagManager.ApplyModifiedProperties();
                            return;
                        }
                    }
                }
            }
        }
    }


    static bool IsHasTag(string tag)
    {
        for (int i = 0; i < UnityEditorInternal.InternalEditorUtility.tags.Length; i++)
        {
            if (UnityEditorInternal.InternalEditorUtility.tags[i].Contains(tag))
                return true;
        }

        return false;
    }

    static bool IsHasLayer(string layer)
    {
        for (int i = 0; i < UnityEditorInternal.InternalEditorUtility.layers.Length; i++)
        {
            if (UnityEditorInternal.InternalEditorUtility.layers[i].Contains(layer))
                return true;
        }

        return false;
    }

    static bool isHasSortingLayer(string sortingLayer)
    {
        SerializedObject tagManager =
            new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty it = tagManager.GetIterator();
        while (it.NextVisible(true))
        {
            if (it.name == "m_SortingLayers")
            {
                for (int i = 0; i < it.arraySize; i++)
                {
                    SerializedProperty dataPoint = it.GetArrayElementAtIndex(i);
                    while (dataPoint.NextVisible(true))
                    {
                        if (dataPoint.name == "name")
                        {
                            if (dataPoint.stringValue == sortingLayer) return true;
                        }
                    }
                }
            }
        }

        return false;
    }
}
