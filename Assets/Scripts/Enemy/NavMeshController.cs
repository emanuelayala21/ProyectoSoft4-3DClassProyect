using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController :MonoBehaviour {

    public Transform objetivo;
    private NavMeshAgent agente;

    void Start() {
        agente = GetComponent<NavMeshAgent>();
    }

    void Update() {
        agente.destination = objetivo.position; 
    }
}
