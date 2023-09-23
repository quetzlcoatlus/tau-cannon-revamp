using UnityEngine;

public class TauGun : MonoBehaviour
{

    public float                    damage              = 10f;
    public float                    range               = 10f;
    public float                    pullFactor          = 200f;
    public float                    pushFactor          = 0f;
    public float                    pushCap             = 70f;
    public float                    knockbackMultiplier = 2f;

    private Camera                  cam;
    public ParticleSystem           muzzleFlash;
    private Transform               magnetPoint;
    public bool                     unPulling;

    public GameObject               heldObject;
    public float                    pushTimer;
    public bool                     isPulling; // Used in pullable controller (pain)

    private Rigidbody                target;

    private GameObject              player;

    private ChargeBarController     chargeBar;

    void Start()
    {
        player          = GameObject.Find("First Person Player");
        magnetPoint     = player.GetComponent<Transform>();
        cam             = Camera.main;
        chargeBar       = GameObject.Find("Charge Bar").GetComponent<ChargeBarController>();
    }

    // Update is called once per frame
    // TODO: Changed logic so there might be some redundant code in this file
    void Update()
    {
        // Checks that the button isn't pressed or there is a held object
        if (!Input.GetButton("Fire1") || heldObject != null)
        {
            if (target != null)
            {
                target.useGravity = true; // Reset gravity for object
            }
            target = null;
            isPulling = false;
        }
        else
        {
            RaycastHit hit;

            // Raycast doesn't hit anything
            if (!Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
            {
                return;
            }

            // Object can't be pulled
            if (!hit.transform.gameObject.GetComponent<PullableController>() || !hit.transform.gameObject.CompareTag("Movable")) // Looks redundant might want to remove the first or second way we test if something is movable
            {
                return;
            }

            isPulling = true;
            Pull(hit);
        }

        // Right click fills charge bar based on time held
        if (Input.GetButton("Fire2"))
        {
            updateChargeLevel();
        }

        // This is allowed while pulling, unintentional but seems interesting
        // Max charge should do some sort of damage eventually
        // Knockback will be based on charge later too
        if (Input.GetMouseButtonUp(1))
        {
            releaseCharge();
        }
    }

    private void releaseCharge()
    {
        pushFactor = pushCap * pushTimer;
        if (pushFactor > pushCap * 2f)
        {
            pushFactor = pushCap * 2f;
        }

        player.GetComponent<PlayerMovement>().knockback(cam.transform.forward.normalized * -1, pushFactor * knockbackMultiplier); // Doing player knockback in the taugun script. bit sus

        Push();
        pushTimer = 0;
        chargeBar.changeCharge(pushTimer);
    }

    private void updateChargeLevel()
    {
        pushTimer += Time.deltaTime;
        Debug.Log(pushTimer);
        chargeBar.changeCharge(pushTimer);
    }


    // When the left mouse button is held and an object is within range it will be pulled towards the Tau Cannon
    // TODO: make it so that the pull doesn't immediately stop an object's velocity(?)
    private void Pull(RaycastHit hit)
    {
        //muzzleFlash.Play();
 
        // Checks when the raycast stops hitting the target to turn its gravity back on
        if (hit.rigidbody != target && target != null)
        {
            target.useGravity = true;
        }

        //DamageSystem target = hit.transform.GetComponent<DamageSystem>();
        target = hit.rigidbody;
        target.useGravity = false;

        if (target != null)
        {
            //target.AddForce((magnetPoint.position + magnetPoint.transform.forward * 0.5f - target.position + new Vector3(0, 0.9f, 0)).normalized * forceFactor * Time.fixedDeltaTime);
            //hit.collider.gameObject.GetComponent<PullableController>().gravityOff();

            target.velocity = (magnetPoint.position + magnetPoint.transform.forward * 0.5f - target.position + new Vector3(0, 0.9f, 0)).normalized * pullFactor * Time.fixedDeltaTime;
        }
    }


    // When there is an object held by the Tau Cannon, applies variable force based on time held and launches the object when the right mouse button is clicked
    private void Push()
    {
        if (heldObject != null)
        {
            Rigidbody heldRigidBody = heldObject.GetComponent<Rigidbody>();
            heldRigidBody.useGravity = true;

            heldObject.GetComponent<PullableController>().PushOff();

            heldRigidBody.velocity = Vector3.zero;
            heldRigidBody.AddForce((cam.transform.forward).normalized * pushFactor, ForceMode.Impulse);
            heldObject = null;
        }
        else
        {
            //play 'nothing held' sound
        }
        //put player knock back here
    }


    public void nowHolding(GameObject thing)
    {
        heldObject = thing;
    }
}
