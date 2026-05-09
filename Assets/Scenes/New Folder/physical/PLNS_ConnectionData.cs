using UnityEngine;

public class PLNS_ConnectionData : MonoBehaviour
{
    public PLNS_DeviceData startDevice;
    public PLNS_DeviceData endDevice;

    public bool isWireless;

    public LineRenderer lineRenderer;

    [Range(0f, 1f)]
    public float packetLossChance = 0.1f;

    [Range(0f, 100f)]
    public float signalStrength = 100f;
}