using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;

public class RoomUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void UpdatePlayerList(Dictionary<int, Player> list)
    {
        playerList.text = "";
        foreach(var entry in list)
        {
            playerList.text += entry.Value.NickName + "\n";
        }
    }
}
