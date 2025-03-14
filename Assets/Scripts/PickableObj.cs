using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObj :MonoBehaviour {

    public bool isPickable = true;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "PlayerInteracZone") other.GetComponentInParent<PickupObj>().objectToPickUp = this.gameObject;
        
    }
    private void OnTriggerExit(Collider other) {
        if(other.tag == "PlayerInteracZone") other.GetComponentInParent<PickupObj>().objectToPickUp = null;

    }
}
