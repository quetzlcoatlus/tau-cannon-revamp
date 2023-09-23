using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullableController : MonoBehaviour
{
    public bool             stuck;

    private GameObject      tauGun;
    private Rigidbody       rigidbod;


    // Start is called before the first frame update
    void Start()
    {
        rigidbod = gameObject.GetComponent<Rigidbody>();
        tauGun = GameObject.Find("tauGun");
    }


    // Update is called once per frame
    void Update()
    {
        if (stuck == true) {
            // transform.position = gun.transform.position + gun.transform.forward * 2.0f;
            transform.position = tauGun.transform.position + tauGun.transform.forward * (6f + 0.55f * transform.localScale.z);
            transform.rotation = tauGun.transform.rotation;
        }
        // find way to turn on gravity when pulling but not stuck
    }


    public void StickTo()
    {
        stuck = true;
    }


    public void PushOff()
    {
        stuck = false;
    }


    public void gravityOff()
    {
        rigidbod.useGravity = false;
    }


    public void gravityOn()
    {
        rigidbod.useGravity = true;
    }


    // NEED TO make sure collisions only checked during pulling
    void OnCollisionEnter(Collision collision){
        if ((collision.gameObject.tag == "Gun" || collision.gameObject.tag == "Player") && tauGun.GetComponent<TauGun>().heldObject == null && tauGun.GetComponent<TauGun>().isPulling == true)
        {
            stuck = true;
            tauGun.GetComponent<TauGun>().nowHolding(gameObject);
        }
    }
}
