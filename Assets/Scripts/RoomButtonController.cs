using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;

public class RoomButtonController : MonoBehaviour
{
    internal string name;
    [SerializeField] private TextMeshProUGUI roomName, playerCount;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void UpdateRoomStatus(RoomInfo info)
    {
        name = info.Name;
        roomName.text = name;
        playerCount.text = "Players: " + info.PlayerCount + "/" + info.MaxPlayers;
    }

    public void JoinRoom()
    {
        Matchmaker.Instance.JoinRoom(name);
    }
}
