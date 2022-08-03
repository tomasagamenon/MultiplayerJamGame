using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform t;
    public Vector3 offset;
    void Update()
    {
        transform.position = t.position + offset;
    }
}
