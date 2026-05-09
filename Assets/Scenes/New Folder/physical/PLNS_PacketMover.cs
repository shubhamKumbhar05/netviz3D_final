using UnityEngine;

public class PLNS_PacketMover : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    public float speed = 5f;

    private float progress = 0f;

    private void Update()
    {
        if (startPoint == null || endPoint == null)
        {
            Destroy(gameObject);
            return;
        }

        progress += Time.deltaTime * speed;

        transform.position = Vector3.Lerp(
            startPoint.position,
            endPoint.position,
            progress
        );

        if (progress >= 1f)
        {
            Destroy(gameObject);
        }
    }
}