using System.Collections.Generic;
using UnityEngine;

public class PLNS_WirelessRingConnection : MonoBehaviour
{
    public Transform startDevice;
    public Transform endDevice;

    public GameObject ringPrefab;

    public float spawnInterval = 1f;

    public float ringExpandSpeed = 2f;

    public float ringLifetime = 2f;

    private float timer = 0f;

    private List<GameObject> activeRings =
        new List<GameObject>();

    private void Update()
    {
        if (startDevice == null || endDevice == null)
        {
            Destroy(gameObject);
            return;
        }

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;

            SpawnRing();
        }

        AnimateRings();
    }

    private void SpawnRing()
    {
        Vector3 midpoint =
            (startDevice.position + endDevice.position) / 2f;

        midpoint.y += 0.2f;

        GameObject ring =
            Instantiate(
                ringPrefab,
                midpoint,
                Quaternion.identity,
                transform
            );

        ring.transform.localScale =
    new Vector3(0.2f, 0.01f, 0.2f);

        activeRings.Add(ring);

        Destroy(ring, ringLifetime);
    }

    private void AnimateRings()
    {
        foreach (GameObject ring in activeRings)
        {
            if (ring == null)
                continue;

            Vector3 scale = ring.transform.localScale;

scale.x += ringExpandSpeed * Time.deltaTime;
scale.z += ringExpandSpeed * Time.deltaTime;

/*
Keep height ultra thin
*/
scale.y = 0.01f;

ring.transform.localScale = scale;
        }
    }
}