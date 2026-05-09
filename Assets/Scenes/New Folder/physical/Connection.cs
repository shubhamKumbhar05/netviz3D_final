using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Connection : MonoBehaviour
{
    public NetworkDevice deviceA;
    public NetworkDevice deviceB;

    public bool isWireless;

    LineRenderer lr;

    void Start()
    {
        lr = GetComponent<LineRenderer>();

        SetupVisual();
    }

    void Update()
    {
        if(deviceA == null || deviceB == null)
            return;

        lr.SetPosition(
            0,
            deviceA.transform.position
        );

        lr.SetPosition(
            1,
            deviceB.transform.position
        );
    }

    void SetupVisual()
    {
        if(isWireless)
        {
            lr.startColor = Color.yellow;
            lr.endColor = Color.yellow;

            lr.widthMultiplier = 0.05f;
        }
        else
        {
            lr.startColor = Color.cyan;
            lr.endColor = Color.cyan;

            lr.widthMultiplier = 0.08f;
        }
    }
}