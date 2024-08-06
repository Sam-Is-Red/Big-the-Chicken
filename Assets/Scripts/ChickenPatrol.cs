using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class ChickenPatrol : MonoBehaviour
{
    private Transform player;

    private Transform Enemy;
    public float speed;
    public float lineOfSite;
    public float rotationSpeed;
    public float range;
    private Boolean CanSeeCorn;
    private Rigidbody chicken;
    [SerializeField] private AudioClip damageSoundClip;
    [SerializeField] private AudioClip climaxSoundClip;
    public AudioSource audioSource;
    public AudioSource audioSource2;
    public float sensedSpeed;

    [SerializeField] private float moveSpeed = 1.0f; 
    private Vector3 targetPosition;
    private int acc;
    private float startPosX;
    private float startPosY;
    private float startPosZ;


    private void Start()
    {
        GetRandomTargetPosition();
        player = GameObject.FindGameObjectWithTag("Apple").transform;
        chicken = GetComponent<Rigidbody>();
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        startPosZ = transform.position.z;
        //Invoke("Spawn Delay", 3);
    }

    private void Update()
    {
        float distanceFromPlayer = Vector3.Distance(player.position, transform.position);
        if (distanceFromPlayer > lineOfSite || CanSeeCorn == true)// if the player is not in the line of sight 
        {
            MoveTowardsTarget(targetPosition);
            audioSource.Stop();
            audioSource2.Stop();
            acc = 0;
        }
        else
        { // if the player can be seen
            if (acc == 0)
            {
                audioSource.clip = damageSoundClip;
                audioSource.Play();
                audioSource2.clip = climaxSoundClip;
                audioSource2.Play();

            }
            acc = acc + 1;

            transform.position = Vector3.MoveTowards(this.transform.position, player.position, sensedSpeed * Time.deltaTime);


            if (targetPosition != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(targetPosition - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }

            if (this.transform.position == player.position) {
                SceneManager.LoadScene("Menu");

            }
            
            //Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
            //MoveTowardsTarget(player);
        }
    }

    private void GetRandomTargetPosition()
    {
        float randomX = UnityEngine.Random.Range(-range, range); // Adjust the range as needed
        float randomZ = UnityEngine.Random.Range(-range, range); // Adjust the range as needed

        targetPosition = new Vector3(randomX + startPosX, startPosY, randomZ + startPosZ);
    }

    private void MoveTowardsTarget(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Rotate to face the target position
        if (targetPosition != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(targetPosition - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        // Check if we've reached the target position
        //if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        if (transform.position == targetPosition && Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            GetRandomTargetPosition(); // Get a new random target position
        }
    }

    public IEnumerator TakeoutBoxReaction()
    {

        CanSeeCorn = true;
        WaitForSeconds wait = new WaitForSeconds(10.0f);

        yield return wait;
        CanSeeCorn = false;
    }


}





/*using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ChickenPatrol : MonoBehaviour
{

    private Transform player;
    private Transform Enemy;
    public float speed;
    public float lineOfSite;
    public float rotationSpeed;
    private Rigidbody chicken;

    [SerializeField] private float moveSpeed = 1.0f; // Adjust the movement speed as needed
    private Vector3 targetPosition;
    

    private void Start()
    {
        GetRandomTargetPosition();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        chicken = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float distanceFromPlayer = Vector3.Distance(player.position, transform.position);
        if (distanceFromPlayer > lineOfSite)// if the player is in the line of sight
        {
            MoveTowardsTarget();
        } else { // if the player cannot be seen
            Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
            transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, speed * Time.deltaTime);

           
            if (targetPosition != Vector3.zero) {

                Quaternion toRotation = Quaternion.LookRotation(targetPosition);
                chicken.MoveRotation(toRotation);

                // transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            }
        }

        
    }

    private void GetRandomTargetPosition()
    {
        float randomX = Random.Range(-15.0f, 15.0f); // Adjust the range as needed
        float randomZ = Random.Range(-15.0f, 15.0f); // Adjust the range as needed

        targetPosition = transform.position + new Vector3(randomX, 0.0f, randomZ);
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Check if we've reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            GetRandomTargetPosition(); // Get a new random target position
        }

        if (targetPosition != Vector3.zero)
        {

            Quaternion toRotation = Quaternion.LookRotation(targetPosition);

            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);


        }
    }
}
*/