using UnityEngine;
using TMPro;

public enum PacketType
{
    HTTP_Request,
    HTTP_Response,
    TLS_ClientHello,
    TLS_ServerHello,
    Encrypted_Data,

    DNS_Query,
    DNS_Response,

    SMTP_Command,
    SMTP_Response,
    FTP_Command,
FTP_Response,
FTP_Data
}

public class Packet : MonoBehaviour
{
    public PacketType type;
    public bool isEncrypted;
    public Transform target;
    public float speed = 5f;

    private System.Action onReach;

    public TextMeshPro label;
    public Transform cameraTransform;

    public string packetData;
    private PacketInfoUI infoUI;

    private float pulseSpeed = 2f;
    private float pulseAmount = 0.05f;

    public void Initialize(PacketType t, string data, bool encrypted, Transform targetObj, System.Action callback)
    {
        type = t;
        packetData = data;
        isEncrypted = encrypted;
        target = targetObj;
        onReach = callback;

        infoUI = FindObjectOfType<PacketInfoUI>();

        SetVisual();
        SetLabel();
    }

    void Start()
    {
        transform.localScale = Vector3.one * 0.3f;

        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target.position,
                speed * Time.deltaTime
            );

            if (cameraTransform != null && label != null)
            {
                label.transform.LookAt(cameraTransform);
                label.transform.Rotate(0, 180, 0);
            }

            float scale = 0.3f + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
            transform.localScale = new Vector3(scale, scale, scale);

            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                onReach?.Invoke();
                Destroy(gameObject);
            }
        }
    }

    void SetVisual()
    {
        Renderer rend = GetComponent<Renderer>();

        Color color = Color.white;

        switch (type)
        {
            case PacketType.HTTP_Request:
                color = Color.blue;
                break;

            case PacketType.HTTP_Response:
                color = Color.magenta;
                break;

            case PacketType.TLS_ClientHello:
            case PacketType.TLS_ServerHello:
                color = Color.yellow;
                break;

            case PacketType.Encrypted_Data:
                color = Color.green;
                break;

            case PacketType.DNS_Query:
                color = new Color(1f, 0.5f, 0f); // ORANGE
                break;

            case PacketType.DNS_Response:
                color = new Color(0f, 1f, 1f); // CYAN
                break;

            case PacketType.SMTP_Command:
    color = new Color(0.6f, 0.3f, 1f); // purple
    break;

case PacketType.SMTP_Response:
    color = new Color(0.3f, 1f, 0.6f); // light green
    break;

case PacketType.FTP_Command:
    color = new Color(0.2f, 0.6f, 1f); // light blue
    break;

case PacketType.FTP_Response:
    color = new Color(1f, 0.8f, 0.2f); // yellowish
    break;

case PacketType.FTP_Data:
    color = new Color(0.2f, 1f, 0.4f); // greenish
    break;

        }

        rend.material.color = color;
        rend.material.EnableKeyword("_EMISSION");
        rend.material.SetColor("_EmissionColor", color * 2f);
    }

    void SetLabel()
    {
        if (label != null)
        {
            string text = type.ToString();

            if (isEncrypted)
                text += " 🔒";

            label.text = text + "\n" + packetData;
        }
    }

    void OnMouseDown()
    {
        Debug.Log("Packet Clicked");

        if (infoUI != null)
        {
            infoUI.ShowInfo(type.ToString(), packetData, isEncrypted);
        }
        else
        {
            Debug.Log("InfoUI is NULL!");
        }
    }
}