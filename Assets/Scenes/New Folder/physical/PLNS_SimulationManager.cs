using TMPro;
using UnityEngine;

public class PLNS_SimulationManager : MonoBehaviour
{
    public static PLNS_SimulationManager Instance;

    [Header("Packet Prefab")]
    public GameObject packetPrefab;

    [Header("Statistics")]
    public TMP_Text packetsSentText;
    public TMP_Text packetsLostText;
    public TMP_Text signalStrengthText;

    private int packetsSent = 0;
    private int packetsLost = 0;

    private bool simulationRunning = false;

    private float timer = 0f;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!simulationRunning)
            return;

        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            timer = 0f;

            SimulateTraffic();
        }
    }

    public void StartSimulation()
    {
        simulationRunning = true;

        PLNS_UIManager.Instance.UpdateStatus(
            "Simulation Started"
        );
    }

    private void SimulateTraffic()
    {
        foreach (
            PLNS_ConnectionData conn
            in PLNS_ConnectionManager.Instance.GetConnections()
        )
        {
            float distance =
                Vector3.Distance(
                    conn.startDevice.transform.position,
                    conn.endDevice.transform.position
                );

            conn.signalStrength =
                Mathf.Clamp(100f - distance * 5f, 10f, 100f);

            signalStrengthText.text =
                "Signal Strength: "
                + Mathf.RoundToInt(conn.signalStrength)
                + "%";

            float random =
                Random.Range(0f, 1f);

            if (random < conn.packetLossChance)
            {
                packetsLost++;

                packetsLostText.text =
                    "Packets Lost: " + packetsLost;

                continue;
            }

            GameObject packet =
                Instantiate(
                    packetPrefab,
                    conn.startDevice.transform.position,
                    Quaternion.identity
                );

            PLNS_PacketMover mover =
                packet.GetComponent<PLNS_PacketMover>();

            mover.startPoint =
                conn.startDevice.transform;

            mover.endPoint =
                conn.endDevice.transform;

            packetsSent++;

            packetsSentText.text =
                "Packets Sent: " + packetsSent;
        }
    }
}