using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DebreeSpawner : MonoBehaviour
{
    public Transform point1, point2;
    public float minRotation = 190f, maxRotation = 350f;
    public float spawnRate = 10f;
    private float spawnCooldown = 0f;
    public GameObject[] debrisPrefs;
    public float minDebreeSpeed, maxDebreeSpeed;
    public PhotonView photonView;

    private void Update()
    {
        if(Time.time >= spawnCooldown)
        {
            Vector2 position = new Vector2(Random.Range(point1.position.x, point2.position.x), 
                Mathf.Lerp(point1.position.y, point2.position.y, 0.5f));
            float rotation = Random.Range(minRotation, maxRotation);
            float speed = Random.Range(minDebreeSpeed, maxDebreeSpeed);
            int random = Random.Range(0, debrisPrefs.Length);
            photonView.RPC("RTC_DebreeSpawn", RpcTarget.All, new object[] { position, rotation, speed , random });
            spawnCooldown = Time.time + 1f / spawnRate;
        }
    }
    [Photon.Pun.RPC]
    private void RTC_DebreeSpawn(Vector2 position, float rotation, float speed, int random)
    {
        Spawn(position, rotation, speed, random);
    }

    void Spawn(Vector2 position, float rotation, float speed, int random)
    {
        Debree debree = Instantiate(debrisPrefs[random], position, Quaternion.Euler(0, 0, rotation)).GetComponent<Debree>();
        debree.speed = speed;
    }
    private void OnDrawGizmosSelected()
    {
        Vector3 a = new Vector3(Mathf.Cos(Mathf.Deg2Rad * minRotation), Mathf.Sin(Mathf.Deg2Rad * minRotation), 0);
        Vector3 b = new Vector3(Mathf.Cos(Mathf.Deg2Rad * maxRotation), Mathf.Sin(Mathf.Deg2Rad * maxRotation), 0);
        Vector3 c = new Vector3(Mathf.Lerp(point1.position.x, point2.position.x, 0.5f), point1.position.y);
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(c, a);
        Gizmos.DrawRay(c, b);
    }
}
