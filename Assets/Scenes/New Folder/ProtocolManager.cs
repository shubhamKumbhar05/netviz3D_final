using UnityEngine;
using TMPro;

public class ProtocolManager : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public NetworkManager networkManager;

    // ===== Setup Containers =====
    public GameObject webSetup;   // HTTP + HTTPS
    public GameObject dnsSetup;
    public GameObject smtpSetup;
    public GameObject ftpSetup;

    void Start()
    {
        DisableAll();
    }

    // ===== Disable All Setups =====
    void DisableAll()
    {
        webSetup.SetActive(false);
        dnsSetup.SetActive(false);
        smtpSetup.SetActive(false);
        ftpSetup.SetActive(false);
    }

    // ===== Main Control =====
    // public void RunSelectedProtocol()
    // {
    //     DisableAll();
    //     ClearPackets();

    //     int choice = dropdown.value;

    //     switch (choice)
    //     {
    //         case 0: // HTTP
    //             webSetup.SetActive(true);
    //             AssignWeb();
    //             networkManager.SendHTTPRequest();
    //             break;

    //         case 1: // HTTPS
    //             webSetup.SetActive(true);
    //             AssignWeb();
    //             networkManager.SendHTTPSRequest();
    //             break;

    //         case 2: // DNS
    //             dnsSetup.SetActive(true);
    //             AssignDNS();
    //             networkManager.StartDNSProcess();
    //             break;

    //         case 3: // SMTP
    //             smtpSetup.SetActive(true);
    //             AssignSMTP();
    //             networkManager.StartSMTP();
    //             break;

    //         case 4: // FTP
    //             ftpSetup.SetActive(true);
    //             AssignFTP();
    //             networkManager.StartFTP();
    //             break;

    //         default:
    //             Debug.Log("Invalid selection");
    //             break;
    //     }
    // }

    public void RunSelectedProtocol()
{
    // ✅ STOP previous simulation FIRST
    networkManager.StopAllSimulations();

    // ✅ Clear old packets
    ClearPackets();

    // ✅ Hide all setups
    DisableAll();

    int choice = dropdown.value;

    switch (choice)
    {
        case 0: // HTTP
            webSetup.SetActive(true);
            AssignWeb();
            networkManager.SendHTTPRequest();
            break;

        case 1: // HTTPS
            webSetup.SetActive(true);
            AssignWeb();
            networkManager.SendHTTPSRequest();
            break;

        case 2: // DNS
            dnsSetup.SetActive(true);
            AssignDNS();
            networkManager.StartDNSProcess();
            break;

        case 3: // SMTP
            smtpSetup.SetActive(true);
            AssignSMTP();
            networkManager.StartSMTP();
            break;

        case 4: // FTP
            ftpSetup.SetActive(true);
            AssignFTP();
            networkManager.StartFTP();
            break;
    }
}

    // ===== Clear Old Packets =====
    void ClearPackets()
    {
        GameObject[] packets = GameObject.FindGameObjectsWithTag("Packet");

        foreach (GameObject p in packets)
        {
            Destroy(p);
        }
    }

    // ===== Assign Functions =====

    void AssignWeb()
    {
        networkManager.client = webSetup.transform.Find("Client");
        networkManager.server = webSetup.transform.Find("Server");
    }

    void AssignDNS()
    {
        networkManager.client = dnsSetup.transform.Find("Client");
        networkManager.server = dnsSetup.transform.Find("Server");

        networkManager.dnsResolver = dnsSetup.transform.Find("DNS_Resolver");
        networkManager.rootServer = dnsSetup.transform.Find("Root_Server");
        networkManager.tldServer = dnsSetup.transform.Find("TLD_Server");
        networkManager.authServer = dnsSetup.transform.Find("Authoritative_Server");
    }

    void AssignSMTP()
    {
        networkManager.client = smtpSetup.transform.Find("Client");
        networkManager.mailServer = smtpSetup.transform.Find("Mail_Server");
    }

    void AssignFTP()
    {
        networkManager.client = ftpSetup.transform.Find("Client");
        networkManager.server = ftpSetup.transform.Find("Server");
    }
}