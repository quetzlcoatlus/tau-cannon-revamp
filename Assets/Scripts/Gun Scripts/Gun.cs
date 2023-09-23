using UnityEngine;

public class Gun : MonoBehaviour
{

    public float            damage      = 10f;
    public float            range       = 100f;

    public Camera           cam;
    public ParticleSystem   muzzleFlash;


    // Update is called once per frame
    void Update()
    {
        //default left click inpput
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }


    void Shoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;

        // If raycast hits something, the object hit's damage system is told to take damage
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            DamageSystem target = hit.transform.GetComponent<DamageSystem>();
            if(target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}
