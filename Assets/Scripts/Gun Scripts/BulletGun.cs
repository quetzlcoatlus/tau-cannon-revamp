using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGun : MonoBehaviour
{

    public GameObject bullet;
 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    // Instantiates a bullet and applies force such that it behaves like it would fired from a weapon
    void Shoot()
    {
        GameObject projectile = Instantiate(bullet) as GameObject;
        projectile.transform.position = transform.position + new Vector3(0,0,1.25f); // Positions the bullet in the barrel of the gun
        projectile.transform.forward = gameObject.transform.forward;
        //projectile.transform.rotation = new Quaternion(90f, 0, 0, 1);
  
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward*20, ForceMode.Impulse);
    }
}
