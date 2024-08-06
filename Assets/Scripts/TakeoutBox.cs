using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeoutBox : MonoBehaviour
{
    public ChickenPatrol chicken;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (other.CompareTag("Player"))
        {
            Debug.Log("WE HAVE COLLid");
            
            StartCoroutine(chicken.TakeoutBoxReaction());

        }
        

    }
}
