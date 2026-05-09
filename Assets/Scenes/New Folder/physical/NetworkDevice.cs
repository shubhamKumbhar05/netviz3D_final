using UnityEngine;

public class NetworkDevice : MonoBehaviour
{
    [Header("Device Info")]
    public string deviceID;

    public bool isServer;

    public float signalStrength = 100f;

    [Header("Visuals")]
    public Renderer rend;

    private Color originalColor;

    // Static counter for unique IDs
    static int counter = 0;

    void Start()
    {
        // Generate unique ID
        counter++;

        deviceID = gameObject.name + "_" + counter;

        // Get renderer automatically
        rend = GetComponentInChildren<Renderer>();

        // Store original color safely
        if(rend != null)
        {
            originalColor = rend.material.color;
        }
    }

    // Highlight selected device
    public void Select()
    {
        if(rend == null)
            return;

        rend.material.EnableKeyword("_EMISSION");

        rend.material.SetColor(
            "_EmissionColor",
            Color.cyan * 3f
        );
    }

    // Remove highlight
    public void Deselect()
    {
        if(rend == null)
            return;

        rend.material.SetColor(
            "_EmissionColor",
            Color.black
        );
    }

    // Optional reset function
    public void ResetColor()
    {
        if(rend == null)
            return;

        rend.material.color = originalColor;
    }
}