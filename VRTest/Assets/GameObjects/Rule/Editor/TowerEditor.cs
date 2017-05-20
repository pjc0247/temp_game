using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TowerData
{
    public string Name;
    public string Description;

    public float interval;
    public float damage;
    public float range;

    public Texture2D thumbnail;
}

public class TowerEditor : EditorWindow {
    [MenuItem("TD/Towers")]
    public static void ShowTowerEditor()
    {
        var window = new TowerEditor();
        window.Show();
    }

    public List<TowerData> towerData;
    private Vector2 scroll;

    public TowerEditor()
    {
        
    }

    void OnEnable()
    {
        towerData = new List<TowerData>();
        towerData.Add(new TowerData()
        {
            Name = "FlameTower",
            Description = "A cute flame tower.",
            thumbnail = Resources.Load<Texture2D>("Editor/Tower/flametower"),

            interval = 0.1f,
            damage = 1.0f,
            range = 2.5f
        });
        towerData.Add(new TowerData()
        {
            Name = "BulletTower",
            Description = "A cute bullet tower.",
            thumbnail = Resources.Load<Texture2D>("Editor/Tower/bullettower"),

            interval = 0.1f,
            damage = 1.0f,
            range = 2.5f
        });
    }
    void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        scroll = EditorGUILayout.BeginScrollView(scroll);

        foreach (var tower in towerData)
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);

            var labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.fixedHeight = 30;
            labelStyle.fontSize = 20;
            labelStyle.alignment = TextAnchor.MiddleRight;
            labelStyle.fontStyle = FontStyle.Bold;

            var dpsLabelStyle = new GUIStyle(labelStyle);
            dpsLabelStyle.fixedHeight = 20;
            dpsLabelStyle.fontSize = 16;

            EditorGUILayout.BeginHorizontal();
            GUILayout.Box(tower.thumbnail, GUILayout.Width(55), GUILayout.Height(55));
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField(tower.Name, labelStyle);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.TextField("Description", tower.Description);
            EditorGUILayout.IntField("Cost", 100);
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal(GUI.skin.box);
            EditorGUILayout.BeginVertical();
            tower.interval = EditorGUILayout.FloatField("Interval", tower.interval);
            tower.damage = EditorGUILayout.FloatField("Damage", tower.damage);
            tower.range = EditorGUILayout.FloatField("Range", tower.range);
            EditorGUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("DPS : " + (tower.damage * (1.0f / tower.interval)).ToString(), dpsLabelStyle);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }
}
