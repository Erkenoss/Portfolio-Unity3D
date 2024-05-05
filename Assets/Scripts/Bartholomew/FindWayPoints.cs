using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FindWayPoints : MonoBehaviour
{
    NavMeshAgent agent;
    Transform player;
    Animator animator;
    public GameObject go;
    public List <Transform> wayPoints = new List <Transform>();
    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator  = GetComponent<Animator>();

        if (go != null) {
            foreach (Transform t in go.transform) {
                wayPoints.Add(t);
            }
        }
    }

    public void Move() {

    }

    private void LootAtPlayer() {
        animator.transform.LookAt(player);
    }
}
