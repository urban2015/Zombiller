using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotator : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool xAxis = false;
    [SerializeField] private bool yAxis = true;
    [SerializeField] private bool zAxis = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.eulerAngles = new Vector3((xAxis)? transform.eulerAngles.x + speed : 0, (yAxis)? transform.eulerAngles.y + speed : 0, (zAxis)? transform.eulerAngles.z + speed : 0);
    }
}
