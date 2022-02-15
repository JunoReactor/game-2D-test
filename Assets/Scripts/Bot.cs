using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    public Animator anim;

    public Transform playerTr;
    NavMeshAgent agent;
    NavMeshPath navMeshPath;

    public float stopDistantion;
    public float retritDistantion;

   
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation    = false;
        agent.updateUpAxis      = false;
        
    }

    void Update()
    {

        playerTr = GameObject.FindGameObjectWithTag("Player").transform;

        agent.SetDestination(playerTr.position);

        if (Vector2.Distance(transform.position, playerTr.position) > stopDistantion)
        {
           // agent.SetDestination(playerTr.position);
        }
        else if(Vector2.Distance(transform.position, playerTr.position) < stopDistantion && Vector2.Distance(transform.position, playerTr.position) < retritDistantion)
        {
            //transform.position = this.transform.position;
        }
        CheckingGround();
    }

    GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Bot");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    public bool onGround;
    public Transform GroundCheck;
    public float checkRadius = 0.5f;
    public LayerMask Ground;

    void CheckingGround()
    {
        onGround = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, Ground);
        anim.SetBool("onGround", onGround);
    }

}