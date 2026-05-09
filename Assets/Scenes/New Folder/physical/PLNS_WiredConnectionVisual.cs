using UnityEngine;

public class PLNS_WiredConnectionVisual : MonoBehaviour
{
    public Transform startDevice;
    public Transform endDevice;

    public Material wireMaterial;

    private Transform cableMesh;

    private void Start()
    {
        GameObject cylinder =
            GameObject.CreatePrimitive(PrimitiveType.Cylinder);

        cylinder.name = "WireCable";

        cylinder.transform.SetParent(transform);

        Destroy(cylinder.GetComponent<Collider>());

        Renderer rend =
            cylinder.GetComponent<Renderer>();

        if (rend != null && wireMaterial != null)
        {
            rend.material = wireMaterial;
        }

        cableMesh = cylinder.transform;
    }

    private void Update()
    {
        if (startDevice == null || endDevice == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 startPos = startDevice.position;
        Vector3 endPos = endDevice.position;

        Vector3 direction = endPos - startPos;

        Vector3 midpoint =
            (startPos + endPos) / 2f;

        cableMesh.position = midpoint;

        cableMesh.up = direction.normalized;

        float distance = direction.magnitude;

        cableMesh.localScale =
            new Vector3(
                0.05f,
                distance / 2f,
                0.05f
            );
    }
}