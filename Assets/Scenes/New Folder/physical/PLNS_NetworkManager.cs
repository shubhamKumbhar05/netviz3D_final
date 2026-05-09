using UnityEngine;

public class PLNS_NetworkManager : MonoBehaviour
{
    public static PLNS_NetworkManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}