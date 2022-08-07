using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public Transform[] spawnPoints;

    private void Start()
    {
        int randomNumber = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomNumber];
        Debug.Log((int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]);
        GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
        GameObject Player = PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, Quaternion.identity);
        FindObjectOfType<Camera>().transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -10);
        FindObjectOfType<Camera>().transform.SetParent(Player.transform);
    }
}
