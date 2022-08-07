using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    public float minTime;
    public float maxTime;
    float _currentTime;
    float _time;

    // Start is called before the first frame update
    void Start()
    {
        _time = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (_currentTime >= _time)
            {
                PhotonNetwork.Instantiate(Enemy.name, transform.position, transform.rotation);
                _time = Random.Range(minTime, maxTime);
                _currentTime = 0;
            }
            else _currentTime += Time.deltaTime;
        }
    }
}
