using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHerd : MonoBehaviour
{
    private List<EnemyBoar> _herd = new List<EnemyBoar>();
    public EnemyBoar enemyBoarPrefab;
    public int maxBoarsInHerd;
    public int minBoarsInHerd;
    private GameObject _alpha;


    void Start()
    {
        for (int i = Random.Range(minBoarsInHerd, maxBoarsInHerd); i >= _herd.Count;)
        {
            Debug.Log(i);
            GameObject newBoar = Instantiate(enemyBoarPrefab.gameObject, transform.position, transform.rotation);
            _herd.Add(newBoar.GetComponent<EnemyBoar>());
            if (_alpha == null)
                _alpha = newBoar;
        }
        foreach (EnemyBoar boar in _herd)
        {
            boar.herd = _herd;
            boar.myHerd = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _alpha.transform.position;
    }
}
