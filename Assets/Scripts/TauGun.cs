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
    public bool                     isPulling;

    public Rigidbody                target;

    private GameObject              player;

    private ChargeBarController     chargeBar;

    void Start()
    {
        player          = GameObject.Find("First Person Player");
        magnetPoint     = GameObject.Find("First Person Player").GetComponent<Transform>();
        cam             = Camera.main;
        chargeBar       = GameObject.Find("Charge Bar").GetComponent<ChargeBarController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Makes sure that only one object can be held. If one
        if (heldObject == null && Input.GetButton("Fire1"))
        {
            isPulling = true;
            Pull();
        }
        else if (target != null)
        {
            target.useGravity = true;
            target = null;
        }

        if (!Input.GetButton("Fire1"))
        {
            isPulling = false;
        }

        if (Input.GetButton("Fire2"))
        {
            
            pushTimer += Time.deltaTime;
            Debug.Log(pushTimer);
            chargeBar.changeCharge(pushTimer);
        }

        // This is allowed while pulling, unintentional but seems interesting
        // Max charge should do some sort of damage eventually
        // Knockback will be based on charge later too
        if (Input.GetMouseButtonUp(1))
        {
            pushFactor = pushCap * pushTimer;
            if (pushFactor > pushCap * 2f)
            {
                pushFactor = pushCap * 2f;
            }

            player.GetComponent<PlayerMovement>().knockback(cam.transform.forward.normalized * -1, pushFactor * knockbackMultiplier);

            Push();
            pushTimer = 0;
            chargeBar.changeCharge(pushTimer);
        }


    }


    // When the left mouse button is held and an object is within range it will be pulled towards the Tau Cannon
    void Pull()
    {
        //muzzleFlash.Play();
        RaycastHit hit;

        // If the raycast hits something, get the target's rigidbody and pull it towards the gun
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            if (hit.transform.gameObject.GetComponent<PullableController>() && hit.transform.gameObject.CompareTag("Movable"))
            {
                // Checks when the raycast stops hitting the object being pulled currently to turn gravity back on
                if (hit.rigidbody != target && target != null)
                {
                    target.useGravity = true;
                }

                //DamageSystem target = hit.transform.GetComponent<DamageSystem>();
                target = hit.rigidbody;

                if (target != null)
                {
                    //target.AddForce((magnetPoint.position + magnetPoint.transform.forward * 0.5f - target.position + new Vector3(0, 0.9f, 0)).normalized * forceFactor * Time.fixedDeltaTime);
                    //hit.collider.gameObject.GetComponent<PullableController>().gravityOff();

                    target.velocity = (magnetPoint.position + magnetPoint.transform.forward * 0.5f - target.position + new Vector3(0, 0.9f, 0)).normalized * pullFactor * Time.fixedDeltaTime;
                    target.useGravity = false;
                }
            }
        }
    }


    // When there is an object held by the Tau Cannon, applies variable force based on time held and launches the object when the right mouse button is clicked
    void Push()
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
