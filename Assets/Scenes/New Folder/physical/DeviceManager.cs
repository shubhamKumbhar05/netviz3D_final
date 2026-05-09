using UnityEngine;
using System.Collections.Generic;

public class DeviceManager : MonoBehaviour
{
    public static DeviceManager instance;

    public GameObject monitorPrefab;
    public GameObject serverPrefab;

    public List<NetworkDevice> allDevices =
        new List<NetworkDevice>();

    void Awake()
    {
        instance = this;
    }

    public void SpawnMonitor()
    {
        GameObject obj = Instantiate(
            monitorPrefab,
            RandomPosition(),
            Quaternion.identity
        );

        obj.name = "Monitor";

        NetworkDevice device =
            obj.GetComponent<NetworkDevice>();

        allDevices.Add(device);
    }

    public void SpawnServer()
    {
        GameObject obj = Instantiate(
            serverPrefab,
            RandomPosition(),
            Quaternion.identity
        );

        obj.name = "Server";

        NetworkDevice device =
            obj.GetComponent<NetworkDevice>();

        allDevices.Add(device);
    }

    Vector3 RandomPosition()
{
    float randomX =
        Random.Range(-2f, 4f);

    float randomZ =
        Random.Range(-3f, 3f);

    return new Vector3(
        randomX,
        0f,
        randomZ
    );
}
}