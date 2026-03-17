using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{
    public Transform client;
    public Transform server;

    public Transform dnsResolver;
    public Transform rootServer;
    public Transform tldServer;
    public Transform authServer;

    public GameObject packetPrefab;
    public UIManager uiManager;
    public Transform mailServer;

    // ================= DNS FLOW =================

    public void StartDNSProcess()
    {
        uiManager.UpdateStatus("Client → DNS Resolver");
        StartCoroutine(Spawn(PacketType.DNS_Query, "example.com ?", false, client, dnsResolver, OnResolver));
    }

    void OnResolver()
    {
        uiManager.UpdateStatus("Resolver → Root");
        StartCoroutine(Spawn(PacketType.DNS_Query, "Where is .com?", false, dnsResolver, rootServer, OnRoot));
    }

    void OnRoot()
    {
        uiManager.UpdateStatus("Root → TLD");
        StartCoroutine(Spawn(PacketType.DNS_Query, ".com location?", false, rootServer, tldServer, OnTLD));
    }

    void OnTLD()
    {
        uiManager.UpdateStatus("TLD → Authoritative");
        StartCoroutine(Spawn(PacketType.DNS_Query, "example.com IP?", false, tldServer, authServer, OnAuth));
    }

    void OnAuth()
    {
        uiManager.UpdateStatus("Authoritative → TLD");
        StartCoroutine(Spawn(PacketType.DNS_Response, "IP = 192.168.1.1", false, authServer, tldServer, OnTLDBack));
    }

    void OnTLDBack()
    {
        uiManager.UpdateStatus("TLD → Root");
        StartCoroutine(Spawn(PacketType.DNS_Response, "Forwarding IP", false, tldServer, rootServer, OnRootBack));
    }

    void OnRootBack()
    {
        uiManager.UpdateStatus("Root → Resolver");
        StartCoroutine(Spawn(PacketType.DNS_Response, "Forwarding IP", false, rootServer, dnsResolver, OnResolverBack));
    }

    void OnResolverBack()
    {
        uiManager.UpdateStatus("Resolver → Client");
        StartCoroutine(Spawn(PacketType.DNS_Response, "example.com = 192.168.1.1", false, dnsResolver, client, OnDNSComplete));
    }

    void OnDNSComplete()
    {
        uiManager.UpdateStatus("DNS Done → Sending HTTP...");
        SendHTTPRequest();
    }

    // ================= HTTP =================

    public void SendHTTPRequest()
    {
        StartCoroutine(Spawn(PacketType.HTTP_Request, "GET /index.html", false, client, server, OnHTTPServer));
    }

    void OnHTTPServer()
    {
        uiManager.UpdateStatus("Server Processing...");
        StartCoroutine(Spawn(PacketType.HTTP_Response, "200 OK", false, server, client, OnHTTPDone));
    }

    void OnHTTPDone()
    {
        uiManager.UpdateStatus("Response Received!");
    }

    // ================= HTTPS =================

public void SendHTTPSRequest()
{
    uiManager.UpdateStatus("Starting TLS Handshake...");
    StartCoroutine(Spawn(PacketType.TLS_ClientHello, "Client Hello", false, client, server, OnServerHello));
}

void OnServerHello()
{
    uiManager.UpdateStatus("Server Hello...");
    StartCoroutine(Spawn(PacketType.TLS_ServerHello, "Server Hello", false, server, client, OnEncryptedReq));
}

void OnEncryptedReq()
{
    uiManager.UpdateStatus("Sending Encrypted Request...");
    StartCoroutine(Spawn(PacketType.Encrypted_Data, "Encrypted GET", true, client, server, OnEncryptedRes));
}

void OnEncryptedRes()
{
    uiManager.UpdateStatus("Receiving Encrypted Response...");
    StartCoroutine(Spawn(PacketType.Encrypted_Data, "Encrypted 200 OK", true, server, client, OnHTTPSDone));
}

void OnHTTPSDone()
{
    uiManager.UpdateStatus("Secure Communication Complete 🔒");
}

// ================= SMTP =================
public void StartSMTP()
{
    uiManager.UpdateStatus("Connecting to Mail Server...");
    StartCoroutine(Spawn(PacketType.SMTP_Command, "CONNECT smtp.server.com", false, client, mailServer, OnSMTPConnect));
}

void OnSMTPConnect()
{
    uiManager.UpdateStatus("Server Ready (220)");
    StartCoroutine(Spawn(PacketType.SMTP_Response, "220 Service Ready", false, mailServer, client, OnHELO));
}

void OnHELO()
{
    uiManager.UpdateStatus("Sending HELO...");
    StartCoroutine(Spawn(PacketType.SMTP_Command, "HELO client.com", false, client, mailServer, OnHELORes));
}

void OnHELORes()
{
    StartCoroutine(Spawn(PacketType.SMTP_Response, "250 Hello", false, mailServer, client, OnMailFrom));
}

void OnMailFrom()
{
    uiManager.UpdateStatus("MAIL FROM...");
    StartCoroutine(Spawn(PacketType.SMTP_Command, "MAIL FROM: user@mail.com", false, client, mailServer, OnMailFromRes));
}

void OnMailFromRes()
{
    StartCoroutine(Spawn(PacketType.SMTP_Response, "250 OK", false, mailServer, client, OnRcptTo));
}

void OnRcptTo()
{
    uiManager.UpdateStatus("RCPT TO...");
    StartCoroutine(Spawn(PacketType.SMTP_Command, "RCPT TO: receiver@mail.com", false, client, mailServer, OnRcptToRes));
}

void OnRcptToRes()
{
    StartCoroutine(Spawn(PacketType.SMTP_Response, "250 OK", false, mailServer, client, OnData));
}

void OnData()
{
    uiManager.UpdateStatus("Sending DATA...");
    StartCoroutine(Spawn(PacketType.SMTP_Command, "DATA", false, client, mailServer, OnDataRes));
}

void OnDataRes()
{
    StartCoroutine(Spawn(PacketType.SMTP_Response, "354 Start Mail Input", false, mailServer, client, OnSendBody));
}

void OnSendBody()
{
    uiManager.UpdateStatus("Sending Email Content...");
    StartCoroutine(Spawn(PacketType.SMTP_Command, "Hello from NetViz3D!", false, client, mailServer, OnBodyRes));
}

void OnBodyRes()
{
    StartCoroutine(Spawn(PacketType.SMTP_Response, "250 Message Accepted", false, mailServer, client, OnSMTPDone));
}

void OnSMTPDone()
{
    uiManager.UpdateStatus("Email Sent Successfully 📧");
}

// ================= FTP =================
public void StartFTP()
{
    uiManager.UpdateStatus("Connecting to FTP Server...");
    StartCoroutine(Spawn(PacketType.FTP_Command, "CONNECT ftp.server.com", false, client, server, OnFTPConnect));
}

void OnFTPConnect()
{
    StartCoroutine(Spawn(PacketType.FTP_Response, "220 FTP Server Ready", false, server, client, OnFTPUser));
}

void OnFTPUser()
{
    uiManager.UpdateStatus("Sending USER...");
    StartCoroutine(Spawn(PacketType.FTP_Command, "USER anonymous", false, client, server, OnFTPUserRes));
}

void OnFTPUserRes()
{
    StartCoroutine(Spawn(PacketType.FTP_Response, "331 Username OK", false, server, client, OnFTPPass));
}

void OnFTPPass()
{
    uiManager.UpdateStatus("Sending PASS...");
    StartCoroutine(Spawn(PacketType.FTP_Command, "PASS guest", false, client, server, OnFTPPassRes));
}

void OnFTPPassRes()
{
    StartCoroutine(Spawn(PacketType.FTP_Response, "230 Login Successful", false, server, client, OnFTPTransfer));
}

void OnFTPTransfer()
{
    uiManager.UpdateStatus("Starting File Upload...");
    StartCoroutine(Spawn(PacketType.FTP_Command, "STOR file.txt", false, client, server, OnFTPDataStart));
}

void OnFTPDataStart()
{
    uiManager.UpdateStatus("Transferring File Data...");

    // Simulate multiple data packets
    StartCoroutine(Spawn(PacketType.FTP_Data, "DATA PACKET 1", false, client, server, null));
    StartCoroutine(Spawn(PacketType.FTP_Data, "DATA PACKET 2", false, client, server, null));
    StartCoroutine(Spawn(PacketType.FTP_Data, "DATA PACKET 3", false, client, server, OnFTPDone));
}

void OnFTPDone()
{
    StartCoroutine(Spawn(PacketType.FTP_Response, "226 Transfer Complete", false, server, client, OnFTPEnd));
}

void OnFTPEnd()
{
    uiManager.UpdateStatus("File Uploaded Successfully 📂");
}

public void StopAllSimulations()
{
    StopAllCoroutines();
}

    // ================= CORE =================

    IEnumerator Spawn(PacketType type, string data, bool encrypted, Transform from, Transform to, System.Action callback)
    {
        yield return new WaitForSeconds(0.6f);

        GameObject obj = Instantiate(packetPrefab, from.position, Quaternion.identity);

        Packet p = obj.GetComponent<Packet>();
        p.Initialize(type, data, encrypted, to, callback);
    }
}