using UnityEngine;
using TMPro;

public class PacketInfoUI : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI dataText;
    public TextMeshProUGUI encryptionText;

    void Start()
    {
        panel.SetActive(true);
    }

    public void ShowInfo(string type, string data, bool encrypted)
    {
        panel.SetActive(true);

        typeText.text = "Type: " + type;
        dataText.text = "Data: " + data;
        encryptionText.text = encrypted ? "Encrypted: YES 🔒" : "Encrypted: NO";
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
    public void Open()
    {
        panel.SetActive(true);
    }
}