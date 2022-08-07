using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerItem : MonoBehaviourPunCallbacks
{
    public TMP_Text playerName;

    public Image backgroundImage;
    public Color highlightColor;
    public GameObject leftArrowButton;
    public GameObject rightArrowButton;

    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    public Image playerAvatar;
    public Sprite[] avatars;

    Player player;

    public int character;

    private void Start()
    {
        playerProperties["playerAvatar"] = 0;
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    private void Update()
    {
        character = (int)playerProperties["playerAvatar"];
    }

    public void SetPlayerInfo(Player _player)
    {
        playerName.text = _player.NickName;
        player = _player;
        UpdatePlayerItem(player);
    }

    public void ApplyLocalChanges()
    {
        backgroundImage.color = highlightColor;
        leftArrowButton.SetActive(true);
        rightArrowButton.SetActive(true);
    }

    public void OnClick_LeftArrow()
    {
        if ((int)playerProperties["playerAvatar"] == 0)
            playerProperties["playerAvatar"] = avatars.Length - 1;
        else
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] - 1;
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void OnClick_RightArrow()
    {
        if ((int)playerProperties["playerAvatar"] == avatars.Length - 1)
            playerProperties["playerAvatar"] = 0;
        else
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] + 1;
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if(player == targetPlayer)
        {
            UpdatePlayerItem(targetPlayer);
        }
    }

    private void UpdatePlayerItem(Player player)
    {
        if (player.CustomProperties.ContainsKey("playerAvatar"))
        {
            playerAvatar.sprite = avatars[(int)player.CustomProperties["playerAvatar"]];
            playerProperties["playerAvatar"] = (int)player.CustomProperties["playerAvatar"];
        }
        else 
            playerProperties["playerAvatar"] = 0;
    }

    public void ChangeAvatar(int number)
    {
        playerProperties["playerAvatar"] = number;
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }
}
