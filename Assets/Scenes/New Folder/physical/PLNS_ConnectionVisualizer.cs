using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PLNS_ConnectionVisualizer : MonoBehaviour
{
    private PLNS_ConnectionData connection;

    private LineRenderer lr;

    private void Awake()
    {
        connection = GetComponent<PLNS_ConnectionData>();

        lr = GetComponent<LineRenderer>();

        connection.lineRenderer = lr;

        lr.positionCount = 2;

        lr.startWidth = 0.12f;
        lr.endWidth = 0.12f;

        lr.useWorldSpace = true;
    }

    private void Update()
    {
        if (connection.startDevice == null ||
            connection.endDevice == null)
        {
            return;
        }

        lr.SetPosition(
            0,
            connection.startDevice.transform.position
        );

        lr.SetPosition(
            1,
            connection.endDevice.transform.position
        );
    }
}