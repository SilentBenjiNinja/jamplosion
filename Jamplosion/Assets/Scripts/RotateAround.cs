using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public float rotationSpeed = 5;
    void Update()
    {
        transform.Rotate(new Vector3(0, 1, 0) * rotationSpeed * Time.deltaTime);
    }
}
