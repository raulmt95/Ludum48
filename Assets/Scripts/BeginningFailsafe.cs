using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginningFailsafe : MonoBehaviour
{
    public GameObject FailsafeTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FailsafeTrigger.SetActive(false);
        }
    }
}
