using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearths :MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            other.GetComponent<Lifes>().Bonus();
            Destroy(gameObject);
        }
    }
}
