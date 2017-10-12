using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowInfo : MonoBehaviour {
    private float damage;
    public float Damage {
        set { damage = value; }
        get { return damage; }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        
        transform.SetParent(collision.transform);
        Rigidbody rb = GetComponent<Rigidbody>();
        Rigidbody colided = collision.gameObject.GetComponent<Rigidbody>();
        if (colided)
        {
            colided.velocity = rb.velocity;
        }
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        rb.useGravity = false;

        BoxCollider bc = GetComponent<BoxCollider>();
        bc.enabled = false;

    }
}
