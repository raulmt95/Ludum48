using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareRotator : MonoBehaviour
{
    public float RotationSpeed;

    private void Update()
    {
        transform.Rotate(Vector3.back * RotationSpeed * Time.deltaTime);
    }
}
