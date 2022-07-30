using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform t;
    void Update()
    {
        transform.position = t.position;
    }
}
