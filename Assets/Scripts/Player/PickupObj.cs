using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObj :MonoBehaviour {

    public GameObject objectToPickUp;
    public GameObject PickedObject;
    public Transform InteractionZone;

    private void Update() {
        if(objectToPickUp != null &&
            objectToPickUp.GetComponent<PickableObj>().isPickable == true &&
            PickedObject == null) {
            if(Input.GetKeyDown(KeyCode.F)) {
                PickedObject = objectToPickUp;
                PickedObject.GetComponent<PickableObj>().isPickable = false;
                PickedObject.transform.SetParent(InteractionZone);
                PickedObject.transform.position = InteractionZone.position;
                PickedObject.GetComponent<Rigidbody>().useGravity = false;
                PickedObject.GetComponent<Rigidbody>().isKinematic = true;
            }
        } else if(PickedObject != null) {
            if(Input.GetKeyDown(KeyCode.F)) {
                PickedObject.GetComponent<PickableObj>().isPickable = true;
                PickedObject.transform.SetParent(null);
                PickedObject.GetComponent<Rigidbody>().useGravity = true;
                PickedObject.GetComponent<Rigidbody>().isKinematic = false;
                PickedObject = null;
            }
        }
    }
}
