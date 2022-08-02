using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTemp : MonoBehaviour
{
    public Transform t;
    public float verticalOffset;
    private void Update()
    {
        transform.position = new Vector3(t.position.x, t.position.y + verticalOffset, -10);
    }
}
