using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI statusText;

    public void UpdateStatus(string msg)
    {
        statusText.text = msg;
    }
}