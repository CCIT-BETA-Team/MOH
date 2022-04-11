using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSpawn : MonoBehaviour
{
    public List<Mission> mission_datas = new List<Mission>();

    [Range(0, 100)]
    public int min_mission_value;
    [Range(0, 100)]
    public int max_mission_value;

    public SpawnPoint[] east_aisa_area;
    public SpawnPoint[] middle_aisa_area;
    public SpawnPoint[] europe_area;
    public SpawnPoint[] africa_area;
    public SpawnPoint[] south_america_area;
    public SpawnPoint[] north_america_area;
    public SpawnPoint[] austrailia_america_area;
    List<SpawnPoint[]> spawn_point_list = new List<SpawnPoint[]>();

    public GameObject mission_object;

    public WhiteBoard wb;
    public InfoCard ic;
    public InformaionPopup ip;
    public Camera mission_camera;

    void Start()
    {
        spawn_card();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < east_aisa_area.Length; i++)
        {
            Gizmos.DrawWireCube(east_aisa_area[i].position + transform.position, east_aisa_area[i].size);
        }
        for (int i = 0; i < middle_aisa_area.Length; i++)
        {
            Gizmos.DrawWireCube(middle_aisa_area[i].position + transform.position, middle_aisa_area[i].size);
        }
        for (int i = 0; i < europe_area.Length; i++)
        {
            Gizmos.DrawWireCube(europe_area[i].position + transform.position, europe_area[i].size);
        }
        for (int i = 0; i < africa_area.Length; i++)
        {
            Gizmos.DrawWireCube(africa_area[i].position + transform.position, africa_area[i].size);
        }
        for (int i = 0; i < south_america_area.Length; i++)
        {
            Gizmos.DrawWireCube(south_america_area[i].position + transform.position, south_america_area[i].size);
        }
        for (int i = 0; i < north_america_area.Length; i++)
        {
            Gizmos.DrawWireCube(north_america_area[i].position + transform.position, north_america_area[i].size);
        }
        for (int i = 0; i < austrailia_america_area.Length; i++)
        {
            Gizmos.DrawWireCube(austrailia_america_area[i].position + transform.position, austrailia_america_area[i].size);
        }
    }

    public void spawn_card()
    {
        int spawn_count = Random.Range(min_mission_value, max_mission_value + 1);
        setting_spawn_list();

        Vector3 spawn_position;
        for (int i = 0; i < spawn_count; i++)
        {
            int index = Random.Range(0, spawn_point_list.Count);
            spawn_position = set_spawn_position(set_point(spawn_point_list[index]));
            spawn_point_list.RemoveAt(index);

            GameObject card = Instantiate(mission_object, spawn_position, Quaternion.identity, transform.parent);
            MissionCard mc = card.GetComponentInChildren<MissionCard>();
            mc.ms = this;
            mc.mi = wb;
            mc.ic = ic;
            mc.ip = ip;
            mc.Setting();
            card.name = "Mission[" + i + "]";
        }
    }

    public Mission get_mission()
    {
        Mission m;
        int i = Random.Range(0, mission_datas.Count);

        m = mission_datas[i];
        mission_datas.RemoveAt(i);

        return m;
    }

    void setting_spawn_list()
    {
        spawn_point_list = new List<SpawnPoint[]>();
        spawn_point_list.Add(east_aisa_area);
        spawn_point_list.Add(middle_aisa_area);
        spawn_point_list.Add(europe_area);
        spawn_point_list.Add(africa_area);
        spawn_point_list.Add(south_america_area);
        spawn_point_list.Add(north_america_area);
        spawn_point_list.Add(austrailia_america_area);
    }

    SpawnPoint set_point(SpawnPoint[] sps)
    {
        SpawnPoint sp = sps[Random.Range(0, sps.Length)];

        return sp;
    }

    Vector3 set_spawn_position(SpawnPoint sp)
    {
        Vector3 position;

        float x = Random.Range(sp.position.x + sp.size.x, sp.position.x - sp.size.x);
        float y = Random.Range(sp.position.y + sp.size.y, sp.position.y - sp.size.y);
        float z = sp.position.z + transform.position.z + 0.01f;

        position = new Vector3(x+ transform.position.x, y+ transform.position.y, z);

        return position;
    }
}

[System.Serializable]
public class SpawnPoint
{
    public Vector3 size;
    public Vector3 position;
}
