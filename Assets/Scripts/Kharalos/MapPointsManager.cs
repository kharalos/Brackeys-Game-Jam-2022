using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapPointsManager : MonoBehaviour
{
    public Transform[] coverPlaces, staffPoints, customerPoints, interactionPoints;
    [SerializeField] List<AIAgent> staffAgents, customerAgents;
    [SerializeField] private float shuffleRate = 10f;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        GetCoverPlaces();
        GetPatrolPointsForStaff();
        GetPatrolPointsForCustomers();
        GetInteractionPoints();
        GetAgents();
        InvokeRepeating("Shuffle", 0.5f, shuffleRate);
    }

    private void GetCoverPlaces(){
        GameObject[] coverPlaceObjects = GameObject.FindGameObjectsWithTag("Cover");
        coverPlaces = new Transform[coverPlaceObjects.Length];
        for (int i = 0; i < coverPlaceObjects.Length; i++){
            coverPlaces[i] = coverPlaceObjects[i].transform;
        }
    }
    private void GetPatrolPointsForStaff()
    {
        GameObject[] staffPointObjects = GameObject.FindGameObjectsWithTag("StaffPatrol");
        staffPoints = new Transform[staffPointObjects.Length];
        for (int i = 0; i < staffPointObjects.Length; i++){
            staffPoints[i] = staffPointObjects[i].transform;
        }
    }
    private void GetPatrolPointsForCustomers()
    {
        GameObject[] customerPointObjects = GameObject.FindGameObjectsWithTag("CustomerPatrol");
        customerPoints = new Transform[customerPointObjects.Length];
        for (int i = 0; i < customerPointObjects.Length; i++){
            customerPoints[i] = customerPointObjects[i].transform;
        }
    }
    private void GetInteractionPoints()
    {
        GameObject[] interactionPointObjects = GameObject.FindGameObjectsWithTag("InteractionPoint");
        interactionPoints = new Transform[interactionPointObjects.Length];
        for (int i = 0; i < interactionPointObjects.Length; i++){
            interactionPoints[i] = interactionPointObjects[i].transform;
        }
    }
    private void GetAgents(){
        staffAgents = new List<AIAgent>();
        customerAgents = new List<AIAgent>();

        foreach(AIAgent agent in FindObjectsOfType<AIAgent>())
        {
            if(agent.CompareTag("Staff"))
            {
                staffAgents.Add(agent);
            }
            else if(agent.CompareTag("Customer"))
            {
                customerAgents.Add(agent);
            }
        }
    }

    private void Shuffle()
    {
        GetAgents();
        customerPoints = customerPoints.OrderBy(x => System.Guid.NewGuid()).ToArray();
        for (int i = 0; i < customerAgents.Count; i++){
            if(customerAgents[i].patrolPoints.Length >0)
                Array.Clear(customerAgents[i].patrolPoints, 0 , customerAgents[i].patrolPoints.Length);
            customerAgents[i].patrolPoints = new Transform[1];
            customerAgents[i].patrolPoints[0] = customerPoints[i];
        }
    }

    public void TaskAssigner(Interactable interactable){
        if(interactable.AIInteractPoint == null) {Debug.Log("Interactable has no assigned Interactable Point for an agent."); return; }

        AIAgent chosenAgent = null;
        float minDist = Mathf.Infinity;

        foreach (AIAgent agent in staffAgents)
        {
            float dist = Vector3.Distance(agent.transform.position, interactable.AIInteractPoint.position);
            if (dist < minDist)
            {
                chosenAgent = agent;
                minDist = dist;
            }
        }

        chosenAgent.interactionPoints = new Transform[1];
        chosenAgent.interactionPoints[0] = interactable.AIInteractPoint;
        chosenAgent.interactable = interactable;
    }
}