using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class aimControl : MonoBehaviour {
    public Image Crosshair;
    public Animator Anim;
    public float ZoomSpeed;
    public Transform FirstPerson;
    public Transform ThirdPerson;
    public bool AimOn;
    private Camera mainCamera;
    private Transform goToPos;
    public GameObject Bow;
    public GameObject Arrow;
    public float RotationSpeedY = 8;
    public float RotationSpeedX = 8;
    public float ShootingFrequency = .5f;
    private bool ready = false;
    private float curTime = 0;
    // Use this for initialization
    void Start () {
        mainCamera = Camera.main;
        goToPos = ThirdPerson;
        Bow.SetActive(false);
        Arrow.SetActive(false);
        Crosshair.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        float mX = Input.GetAxis("Mouse X") * RotationSpeedX;
        float mY = 0;
        if (AimOn)
        {

            mY = -Input.GetAxis("Mouse Y") * RotationSpeedY;
        }
        transform.Rotate(new Vector3(mY, mX, 0));
        Vector3 rot = transform.localEulerAngles;
        rot.z = 0;
        if (AimOn)
        {
            bool changed = false;
            if (rot.x > 270) {
                changed = true;
                rot.x = rot.x - 360;
            }
            rot.x = Mathf.Clamp(rot.x, -30, 30);
            if (changed) {
                rot.x += 360;
            }
        }
        else {
            rot.x = 0;
        }
        transform.localEulerAngles = rot;


        if (curTime < ShootingFrequency)
        {
            curTime += Time.deltaTime;
            if (curTime > ShootingFrequency) {
                ready = true;
            }
        }
        if (ready)
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<playermovement>().enabled = false;
                Anim.SetFloat("Speedx", 0);
                Anim.SetFloat("Speedy", 0);
                Anim.SetTrigger("DrawArrow");
                Bow.SetActive(true);


            }
            if (Input.GetMouseButtonUp(0))
            {

                Anim.SetTrigger("Shoot");
                StartCoroutine(AfterShoot());
                ready = false;

            }
        }

        if (Input.GetButtonDown("Camera Change"))
        {
            
            AimOn = !AimOn;
            if (!AimOn)
            {
                Crosshair.enabled = false;
            }
            StartCoroutine(GetBowAnim());
            
        }
        
        float dist = Vector3.Distance(mainCamera.transform.position, goToPos.position);
        if (dist > .1f)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, goToPos.position, ZoomSpeed * Time.deltaTime);
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, goToPos.rotation, ZoomSpeed * Time.deltaTime);
        }
        
    }
    bool done;
    IEnumerator AfterShoot() {
        done = false;
        if (AimOn) {
            GetComponent<Shooting>().Shoot();
            Arrow.SetActive(false);
            done = true;
        }
        yield return new WaitForSeconds(.3f);
        if (!AimOn)
        {
            Arrow.SetActive(true);
        }
        yield return new WaitForSeconds(.5f);
        if (AimOn)
        {
            Arrow.SetActive(true);
        }
        yield return new WaitForSeconds(.3f);
        if (!AimOn && !done)
        {
            GetComponent<Shooting>().Shoot();
            Arrow.SetActive(false);
        }
        yield return new WaitForSeconds(.6f);
        
        //Anim.SetTrigger("Equip");
        //Anim.SetBool("HasBow", AimOn);
        if (AimOn)
        {
            goToPos = FirstPerson;
            Bow.SetActive(true);
            
        }
        else
        {
            goToPos = ThirdPerson;
            Bow.SetActive(false);
        }
        GetComponent<playermovement>().enabled = true;
        curTime = 0;
    }
    IEnumerator GetBowAnim() {
        Anim.SetTrigger("Equip");
        Anim.SetBool("HasBow", AimOn);
        yield return new WaitForSeconds(.5f);
        
        if (AimOn)
        {
            Crosshair.enabled = true;
            Arrow.SetActive(true);
            goToPos = FirstPerson;
            Bow.SetActive(true);
            
        }
        else
        {
            Arrow.SetActive(false);
            goToPos = ThirdPerson;
            Bow.SetActive(false);
            Vector3 curY = new Vector3(0,transform.localEulerAngles.y,0);

            transform.localEulerAngles = curY;
        }
    }
}
