using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ConnectionManager : MonoBehaviour
{
    public static ConnectionManager instance;

    public GameObject connectionPrefab;

    public TMP_Dropdown startDropdown;
    public TMP_Dropdown endDropdown;

    public List<Connection> allConnections =
        new List<Connection>();

    void Awake()
    {
        instance = this;
    }

    public void CreateWiredConnection()
    {
        CreateConnection(false);
    }

    public void CreateWirelessConnection()
    {
        CreateConnection(true);
    }

    void CreateConnection(bool wireless)
    {
        if(DeviceManager.instance.allDevices.Count < 2)
            return;

        int startIndex = startDropdown.value;
        int endIndex = endDropdown.value;

        if(startIndex == endIndex)
            return;

        NetworkDevice deviceA =
            DeviceManager.instance
            .allDevices[startIndex];

        NetworkDevice deviceB =
            DeviceManager.instance
            .allDevices[endIndex];

        GameObject obj =
            Instantiate(connectionPrefab);

        Connection conn =
            obj.GetComponent<Connection>();

        conn.deviceA = deviceA;
        conn.deviceB = deviceB;
        conn.isWireless = wireless;

        allConnections.Add(conn);
    }
}