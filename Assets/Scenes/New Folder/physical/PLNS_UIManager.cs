using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PLNS_UIManager : MonoBehaviour
{
    public static PLNS_UIManager Instance;

    [Header("Dropdowns")]
    public TMP_Dropdown startDropdown;
    public TMP_Dropdown endDropdown;

    [Header("Status")]
    public TMP_Text statusText;

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

    public void RefreshDeviceDropdowns()
    {
        startDropdown.ClearOptions();
        endDropdown.ClearOptions();

        List<string> names = new List<string>();

        foreach (PLNS_DeviceData device in PLNS_DeviceManager.Instance.GetAllDevices())
        {
            names.Add(device.deviceName);
        }

        startDropdown.AddOptions(names);
        endDropdown.AddOptions(names);
    }

    public void UpdateStatus(string message)
    {
        statusText.text = message;
    }
}