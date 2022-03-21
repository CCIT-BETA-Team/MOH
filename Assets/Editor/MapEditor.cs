//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//[CustomEditor(typeof(Map))]
//public class MapEditor : Editor
//{
//    Map map;
//    SerializedProperty room_list;
//    SerializedProperty telephone_list;

//    void OnEnable()
//    {
//        map = target as Map;
//        room_list = serializedObject.FindProperty("room_list");
//        telephone_list = serializedObject.FindProperty("telephone_list");
//    }

//    public override void OnInspectorGUI()
//    {
//        //DrawDefaultInspector();
//        EditorGUILayout.LabelField("Map Option", EditorStyles.boldLabel);
//        map.npc_amount = EditorGUILayout.IntField("NPC Amout", map.npc_amount);
//        EditorGUILayout.PropertyField(room_list, new GUIContent("Room List"), true);
//        EditorGUILayout.PropertyField(telephone_list, new GUIContent("Telephone List"), true);
//        map.police_npc = EditorGUILayout.ObjectField("Police NPC Object", map.police_npc, typeof(GameObject), true) as GameObject;
//        EditorGUILayout.Space(15);

//        EditorGUILayout.LabelField("Room Data", EditorStyles.boldLabel);
//        map.bedroom_data = EditorGUILayout.ObjectField("Bedroom Data", map.bedroom_data, typeof(RoomData), true) as RoomData;
//        map.livingroom_data = EditorGUILayout.ObjectField("Livingroom Data", map.livingroom_data, typeof(RoomData), true) as RoomData;
//        map.diningroom_data = EditorGUILayout.ObjectField("Diningroom Data", map.diningroom_data, typeof(RoomData), true) as RoomData;
//        map.bathroom_data = EditorGUILayout.ObjectField("Bathroom Data", map.bathroom_data, typeof(RoomData), true) as RoomData;
//        map.office_data = EditorGUILayout.ObjectField("Office Data", map.office_data, typeof(RoomData), true) as RoomData;
//        map.nursery_data = EditorGUILayout.ObjectField("Nursery Data", map.nursery_data, typeof(RoomData), true) as RoomData;
//        map.dressingroom_data = EditorGUILayout.ObjectField("Dressingroom Data", map.dressingroom_data, typeof(RoomData), true) as RoomData;
//        map.laundryroom_data = EditorGUILayout.ObjectField("Laundryroom Data", map.laundryroom_data, typeof(RoomData), true) as RoomData;
//        EditorGUILayout.Space(15);

//        EditorGUILayout.LabelField("Map Node Option", EditorStyles.boldLabel);
//        map.MD = EditorGUILayout.ObjectField("MapData", map.MD, typeof(MapData), true) as MapData;

//        EditorGUILayout.LabelField("Map Size", EditorStyles.boldLabel);
//        map.map_x = EditorGUILayout.IntField("Map Width", map.map_x);
//        map.map_y = EditorGUILayout.IntField("Map Height", map.map_y);
//        if (map.map_x <= 0) { map.map_x = 1; }
//        if (map.map_y <= 0) { map.map_y = 1; }
//        EditorGUILayout.Space(15);

//        EditorGUILayout.LabelField("Map Node", EditorStyles.boldLabel);
//        Map_Node();
//        if (GUILayout.Button("Save Map Node")) { map.Save_Node(); }
//        if (GUILayout.Button("Load Map Node")) { map.Load_Node(); }
//    }

//    void Map_Node()
//    {
//        map.map_size();

//        for (int i = 0; i < map.map_y; i++)
//        {
//            EditorGUILayout.BeginHorizontal();
//            for (int j = 0; j < map.map_x; j++)
//            {
//                map.map_node[i][j] = EditorGUILayout.ObjectField(map.map_node[i][j], typeof(GameObject), true, GUILayout.Width(50), GUILayout.Height(50)) as GameObject;
//            }
//            EditorGUILayout.EndHorizontal();
//        }
//    }
//}
