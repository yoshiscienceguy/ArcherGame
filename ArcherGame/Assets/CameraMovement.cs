using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    private float curMult;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Run"))
        {
            curMult += 1.5f * Time.deltaTime;


            if (curMult >= .2f)
            {
                curMult = .2f;
            }
        }

        else
        {
            curMult -= 1.5f * Time.deltaTime;
            if (curMult <= 0)
            {
                curMult = 0;
            }
        }

        Vector3 curLoc = transform.localPosition ;
        curLoc.z -= curMult;
        transform.localPosition = curLoc;

    }
}
