using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushRigidBody :MonoBehaviour {
    public float pushPower = 4;
    public float targetMass; 
    private void OnControllerColliderHit(ControllerColliderHit hit) {
        Rigidbody body = hit.collider.attachedRigidbody;

        if(body == null || body.isKinematic || hit.moveDirection.y < 0.3) return;

        Vector3 pushDir =  new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        targetMass = body.mass;
        
        body.velocity = pushDir * pushPower / targetMass;

    }
}
