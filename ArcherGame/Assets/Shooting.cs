using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {
    public GameObject ArrowToSpawn;
    public Transform ArrowLocation;
    public float ArrowSpeed = 15;
    public float ArrowDamage = 10;

    public void Shoot() {
        GameObject spawnedArrow = (GameObject)Instantiate(ArrowToSpawn, ArrowLocation.position, ArrowLocation.rotation);
        Rigidbody ArrowRB = spawnedArrow.GetComponent<Rigidbody>();
        ArrowRB.AddForce(transform.TransformDirection(Vector3.forward) * ArrowSpeed, ForceMode.Impulse);
        ArrowInfo info = spawnedArrow.GetComponent<ArrowInfo>();
        info.Damage = ArrowDamage;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
}
