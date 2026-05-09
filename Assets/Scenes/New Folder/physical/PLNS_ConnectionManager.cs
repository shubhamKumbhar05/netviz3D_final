using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PLNS_ConnectionManager : MonoBehaviour
{
    public static PLNS_ConnectionManager Instance;

    [Header("Parents")]
    public Transform connectionsParent;

    [Header("Materials")]
    public Material wiredMaterial;

    [Header("Wireless")]
    public GameObject wirelessRingPrefab;

    [Header("UI")]
    public TMP_Dropdown connectionTypeDropdown;

    private List<PLNS_ConnectionData> allConnections =
        new List<PLNS_ConnectionData>();

    private void Awake()
    {
        Instance = this;
    }

    public List<PLNS_ConnectionData> GetConnections()
    {
        return allConnections;
    }

    public void CreateConnection()
    {
        int startIndex =
            PLNS_UIManager.Instance.startDropdown.value;

        int endIndex =
            PLNS_UIManager.Instance.endDropdown.value;

        if (startIndex == endIndex)
        {
            PLNS_UIManager.Instance.UpdateStatus(
                "Cannot connect same device"
            );

            return;
        }

        PLNS_DeviceData startDevice =
            PLNS_DeviceManager.Instance
            .GetAllDevices()[startIndex];

        PLNS_DeviceData endDevice =
            PLNS_DeviceManager.Instance
            .GetAllDevices()[endIndex];

        bool wireless =
            connectionTypeDropdown.value == 1;

        GameObject connectionObj =
            new GameObject(
                wireless
                ? "WirelessConnection"
                : "WiredConnection"
            );

        connectionObj.transform.SetParent(
            connectionsParent
        );

        PLNS_ConnectionData data =
            connectionObj.AddComponent<PLNS_ConnectionData>();

        data.startDevice = startDevice;
        data.endDevice = endDevice;
        data.isWireless = wireless;

        if (wireless)
        {
            PLNS_WirelessRingConnection visual =
                connectionObj.AddComponent
                <PLNS_WirelessRingConnection>();

            visual.startDevice =
                startDevice.transform;

            visual.endDevice =
                endDevice.transform;

            visual.ringPrefab =
                wirelessRingPrefab;
        }
        else
        {
            PLNS_WiredConnectionVisual visual =
                connectionObj.AddComponent
                <PLNS_WiredConnectionVisual>();

            visual.startDevice =
                startDevice.transform;

            visual.endDevice =
                endDevice.transform;

            visual.wireMaterial =
                wiredMaterial;
        }

        allConnections.Add(data);

        PLNS_UIManager.Instance.UpdateStatus(
            wireless
            ? "Wireless connection created"
            : "Wired connection created"
        );
    }

    public void DeleteAllConnections()
    {
        foreach (
            PLNS_ConnectionData conn
            in allConnections
        )
        {
            if (conn != null)
            {
                Destroy(conn.gameObject);
            }
        }

        allConnections.Clear();

        PLNS_UIManager.Instance.UpdateStatus(
            "All connections deleted"
        );
    }
}