using System.Collections.Generic;
using UnityEngine;

public class PLNS_DeviceManager : MonoBehaviour
{
    public static PLNS_DeviceManager Instance;

    [Header("Device Prefabs")]
    public GameObject monitorPrefab;
    public GameObject serverPrefab;

    [Header("Spawn Parent")]
    public Transform devicesParent;

    private List<PLNS_DeviceData> allDevices = new List<PLNS_DeviceData>();

    private int monitorCount = 0;
    private int serverCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public List<PLNS_DeviceData> GetAllDevices()
    {
        return allDevices;
    }

    public void SpawnMonitor()
    {
        Vector3 spawnPos = GetRandomSpawnPosition();

        GameObject obj = Instantiate(
            monitorPrefab,
            spawnPos,
            Quaternion.identity,
            devicesParent
        );

        monitorCount++;

        PLNS_DeviceData data = obj.GetComponent<PLNS_DeviceData>();

        data.deviceID = "MON_" + monitorCount;
        data.deviceName = "Monitor " + monitorCount;
        data.deviceType = PLNS_DeviceType.Monitor;

        allDevices.Add(data);

        PLNS_UIManager.Instance.RefreshDeviceDropdowns();
        PLNS_UIManager.Instance.UpdateStatus("Spawned " + data.deviceName);
    }

    public void SpawnServer()
    {
        Vector3 spawnPos = GetRandomSpawnPosition();

        GameObject obj = Instantiate(
            serverPrefab,
            spawnPos,
            Quaternion.identity,
            devicesParent
        );

        serverCount++;

        PLNS_DeviceData data = obj.GetComponent<PLNS_DeviceData>();

        data.deviceID = "SRV_" + serverCount;
        data.deviceName = "Server " + serverCount;
        data.deviceType = PLNS_DeviceType.Server;

        allDevices.Add(data);

        PLNS_UIManager.Instance.RefreshDeviceDropdowns();
        PLNS_UIManager.Instance.UpdateStatus("Spawned " + data.deviceName);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float x = Random.Range(-2f, 4f);
        float z = Random.Range(-3f, 3f);

        return new Vector3(x, 0f, z);
    }
}