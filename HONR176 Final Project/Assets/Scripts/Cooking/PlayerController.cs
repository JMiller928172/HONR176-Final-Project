using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("Pickup Settings")]
    public KeyCode pickupKey = KeyCode.E;
    public float pickupRange = 2f;
    public Transform holdPoint;

    private Rigidbody rb;
    private Rigidbody heldObject;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // prevent physics rotation
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandlePickup();
    }

    void HandleMovement()
    {
        float h = Input.GetAxisRaw("Horizontal"); // A/D
        float v = Input.GetAxisRaw("Vertical");   // W/S

        Vector3 move = new Vector3(h, 0, v).normalized * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + move);
    }

    void HandleRotation()
    {
        // Ray from camera to mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        float rayDistance;
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Vector3 direction = (point - transform.position).normalized;
            direction.y = 0; // keep rotation flat on ground

            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                rb.MoveRotation(lookRotation);
            }
        }
    }

    void HandlePickup()
    {
        if (Input.GetKeyDown(pickupKey))
        {
            if (heldObject == null)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, pickupRange);
                foreach (Collider col in colliders)
                {
                    Rigidbody targetRb = col.attachedRigidbody;
                    if (targetRb != null && targetRb != rb)
                    {
                        PickUpObject(targetRb);
                        break;
                    }
                }
            }
            else
            {
                DropObject();
            }
        }
    }

    void PickUpObject(Rigidbody obj)
    {
        heldObject = obj;
        heldObject.useGravity = false;
        heldObject.isKinematic = true;
        heldObject.transform.position = holdPoint.position;
        heldObject.transform.parent = holdPoint;
        heldObject.transform.rotation = holdPoint.rotation;
    }

    void DropObject()
    {
        heldObject.useGravity = true;
        heldObject.isKinematic = false;
        heldObject.transform.parent = null;
        heldObject = null;
    }
}