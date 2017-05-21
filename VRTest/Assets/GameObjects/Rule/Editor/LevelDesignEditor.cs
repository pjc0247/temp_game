using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Newtonsoft.Json;

public class LevelDesignEditor : EditorWindow {
   
    [MenuItem("TD/LevelDesignEditor")]
    public static void Open()
    {
        var window = new LevelDesignEditor();
        window.titleContent.text = "LevelDesignEditor";
        window.Show();
    }

    private static string Path = "Assets/Resources/Data/LevelDesign.json";

    private LevelDesignData data;
    private Vector2 scroll;

    void Setup()
    {
        try
        {
            if (File.Exists(Path))
            {
                var json = File.ReadAllText(Path);
                data = JsonConvert.DeserializeObject<LevelDesignData>(json);
            }
            else data = new LevelDesignData();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            data = new LevelDesignData();
        }
    }
    void Save()
    {
        var json = JsonConvert.SerializeObject(data);
        File.WriteAllText(Path, json);
    }

    void OnGUI()
    {
        if (data == null) Setup();

        var skin = GUI.skin.label;
        skin.richText = true;

        EditorGUILayout.BeginVertical();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("TotalLevels : " + data.levels.Count.ToString());
        if (GUILayout.Button("Add"))
        {
            data.levels.Add(new LevelData()
            {
                level = data.levels.Count
            });
        }
        EditorGUILayout.EndHorizontal();

        scroll = EditorGUILayout.BeginScrollView(scroll);
        foreach (var item in data.levels)
        {
            EditorGUI.indentLevel++;

            var boxSkin = new GUIStyle(GUI.skin.box);
            boxSkin.stretchWidth = true;
            EditorGUILayout.BeginVertical(boxSkin);
            EditorGUILayout.LabelField("<b>Level " + (item.level+1) + "</b>", skin);

            item.spawnAmount = EditorGUILayout.IntField("Amount", item.spawnAmount);
            item.spawnCount = EditorGUILayout.IntField("Count", item.spawnCount);
            item.spawnInterval = EditorGUILayout.FloatField("Interval", item.spawnInterval);

            EditorGUI.indentLevel--;
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndScrollView();

        EditorGUILayout.EndVertical();

        if (GUI.changed)
            Save();
    }
}
