using UnityEngine;
using Photon.Pun;

public class GameLauncher : MonoBehaviourPunCallbacks
{
    

    [SerializeField] private string gameVersion = "0.0.1";


    private void Awake()
    {
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        Connect();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Start the connection process. Connects the client to photon cloud network.
    /// </summary>
    private void Connect()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }

    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to server!");
    }

    public override void OnDisconnected(Photon.Realtime.DisconnectCause cause)
    {
        Debug.Log("Disconnected from server. Log:\n" + cause.ToString());
    }
}
