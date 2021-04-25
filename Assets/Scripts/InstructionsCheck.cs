using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsCheck : MonoBehaviour
{
    public GameObject MoveKeys;
    public GameObject ShootKeys;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveKeys.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ShootKeys.SetActive(false);
        }
    }
}
