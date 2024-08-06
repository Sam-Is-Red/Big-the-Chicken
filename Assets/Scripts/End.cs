using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Menu");

        }


    }
}
