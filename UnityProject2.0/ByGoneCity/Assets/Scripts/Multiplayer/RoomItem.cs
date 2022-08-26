using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomItem : MonoBehaviour
{
    public TMP_Text roomName;
    private LobbyManager _lobbyManager;

    private void Start()
    {
        _lobbyManager = FindObjectOfType<LobbyManager>();
    }

    public void SetRoomName(string _roomName)
    {
        roomName.text = _roomName;
    }

    public void OnClick_Item()
    {
        _lobbyManager.JoinRoom(roomName.text);
    }
}
