using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoamingChicken : MonoBehaviour   
{

    public NavMeshAgent chicken;
    public float range; // radius of roaming area 

    public Transform centrePoint;


    // Start is called before the first frame update
    void Start()
    {
        chicken = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
       

        if (chicken.remainingDistance <= chicken.stoppingDistance) { 
            Vector3 point;
            print("Nav Mesh On!");
            if (RandomPoint(centrePoint.position, range, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                    chicken.SetDestination(point);
            }
        }
            
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result) {

        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;

    }
}
