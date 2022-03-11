using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Map))]
public class MapEditor : Editor
{
    Map map;

    void OnEnable()
    {
        map = target as Map;
    }

    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();
        EditorGUILayout.LabelField("Map Option", EditorStyles.boldLabel);
        map.MD = EditorGUILayout.ObjectField("MapData", map.MD, typeof(MapData), true) as MapData;

        EditorGUILayout.LabelField("Map Option", EditorStyles.boldLabel);
        map.npc_amount = EditorGUILayout.IntField("NPC Amout", map.npc_amount);
        EditorGUILayout.Space(15);

        EditorGUILayout.LabelField("Map Size", EditorStyles.boldLabel);
        map.map_x = EditorGUILayout.IntField("Map Width",map.map_x);
        map.map_y = EditorGUILayout.IntField("Map Height", map.map_y);
        if (map.map_x <= 0) { map.map_x = 1; }
        if (map.map_y <= 0) { map.map_y = 1; }
        EditorGUILayout.Space(15);

        EditorGUILayout.LabelField("Map Node", EditorStyles.boldLabel);
        Map_Node();
        if(GUILayout.Button("Save Map Node")) { map.Save_Node(); }
        if(GUILayout.Button("Load Map Node")) { map.Load_Node(); }
    }

    void Map_Node() 
    {
        map.map_size();

        for (int i = 0; i < map.map_y; i++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < map.map_x; j++)
            {
                map.map_node[i][j] = EditorGUILayout.ObjectField(map.map_node[i][j], typeof(GameObject), true, GUILayout.Width(50), GUILayout.Height(50)) as GameObject;
            }
            EditorGUILayout.EndHorizontal(); 
        }
    }
}
