using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHerd : MonoBehaviour
{
    private List<EnemyBoar> _herd;
    public EnemyBoar enemyBoarPrefab;
    public int maxBoarsInHerd;
    public int minBoarsInHerd;


    void Start()
    {
        for (int i = Random.Range(minBoarsInHerd, maxBoarsInHerd); i <= _herd.Count;)
        {
            EnemyBoar newBoar = Instantiate(enemyBoarPrefab, transform);
            _herd.Add(newBoar);
        }
        foreach (EnemyBoar boar in _herd)
        {
            boar.herd = _herd;
            boar.myHerd = this;
        }
    }

    void Update()
    {
        List<Transform> transforms = new List<Transform>();
        foreach (EnemyBoar boar in _herd)
            transforms.Add(boar.transform);
        transform.position = FindCenterOfTransforms(transforms);


        //var totalX = 0f;
        //var totalY = 0f;
        //foreach (EnemyBoar boar in _herd)
        //{
        //    totalX += boar.transform.position.x;
        //    totalY += boar.transform.position.y;
        //}
        //var centerX = totalX / _herd.Count;
        //var centerY = totalY / _herd.Count;
        //transform.position = new Vector3(centerX, centerY, 0f);
    }

    public Vector3 FindCenterOfTransforms(List<Transform> transforms)
    {
        var bound = new Bounds(transforms[0].position, Vector3.zero);
        for (int i = 1; i < transforms.Count; i++)
        {
            bound.Encapsulate(transforms[i].position);
        }
        return bound.center;
    }
}
