using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage :MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            other.GetComponent<Lifes>().Lost();
        }
    }
}