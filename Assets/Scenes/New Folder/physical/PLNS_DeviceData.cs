using UnityEngine;

public class PLNS_DeviceData : MonoBehaviour
{
    public string deviceID;
    public string deviceName;
    public PLNS_DeviceType deviceType;
}

public enum PLNS_DeviceType
{
    Monitor,
    Server
}