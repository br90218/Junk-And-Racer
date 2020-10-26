using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Matchmaker : MonoBehaviourPunCallbacks
{
    public static Matchmaker Instance;


    [SerializeField] private Transform roomListContainer;
    [SerializeField] private GameObject roomButtonPrefab;
    [SerializeField] private List<RoomButtonController> rooms = new List<RoomButtonController>();

    [SerializeField] private RoomUIController roomUI;
    
    
    
    private string defaultRoomName;
    private Room room;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void Start() {
        defaultRoomName = "New Room #" + Random.Range(0,99);
    }

    /// <summary>
    /// Connects to the default lobby first. Then lists out the room through PUN callbacks.
    /// </summary>
    public void ListRooms()
    {
        PhotonNetwork.JoinLobby();
        
    }

    /// <summary>
    /// Creates a room with default room settings.
    /// </summary>
    public void CreateRoom()
    {
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 8;
        PhotonNetwork.CreateRoom(defaultRoomName, roomOptions);
    }

    /// <summary>
    /// Joins a room.
    /// </summary>
    public void JoinRoom(string name)
    {
        PhotonNetwork.JoinRoom(name);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Retrieving Lobby Information...");
        foreach(var info in roomList)
        {
            if(info.RemovedFromList)
            {
                var roomIndex = rooms.FindIndex( x => x.name == info.Name);
                if(roomIndex != -1)
                {
                    rooms.RemoveAt(roomIndex);
                }
            }
            else{
                RoomButtonController button = Instantiate(roomButtonPrefab, roomListContainer).GetComponent<RoomButtonController>();
                button.UpdateRoomStatus(info);
                rooms.Add(button);
            }
        }
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("There has been an error creating a room: " + returnCode + message );
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Client has joined lobby");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Client has joined room");
        roomUI.gameObject.SetActive(true);
        roomUI.UpdatePlayerList(PhotonNetwork.CurrentRoom.Players);
    }

    public void PlayerReady()
    {

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        roomUI.UpdatePlayerList(PhotonNetwork.CurrentRoom.Players);
    }

    public void SetRoomName(string value)
    {
        defaultRoomName = value;
    }

    public void SetPlayerName(string value)
    {
        PhotonNetwork.NickName = value;
    }


}
