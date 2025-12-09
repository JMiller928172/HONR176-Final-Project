using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float maxSpeed = 10f;

    [Header("Pickup Settings")]
    public KeyCode pickupKey = KeyCode.E;
    public float pickupRange = 2f;
    public Transform holdPoint;

    [Header("UI Settings")]
    public TextMeshProUGUI pickupText;

    private Rigidbody rb;
    private Rigidbody heldObject;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        if (pickupText != null)
            pickupText.gameObject.SetActive(false); 
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandlePickup();
        HandlePickupPrompt();
    }

    void HandleMovement()
    {
        float h = Input.GetAxisRaw("Horizontal"); 
        float v = Input.GetAxisRaw("Vertical"); 

        Vector3 moveDir = new Vector3(h, 0, v).normalized;

        rb.AddForce(moveDir * moveSpeed, ForceMode.Acceleration);

        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    void HandleRotation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        float rayDistance;
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Vector3 direction = (point - transform.position).normalized;
            direction.y = 0;

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

    void HandlePickupPrompt()
    {
        if (pickupText == null) return;

        if (heldObject == null)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, pickupRange);
            bool foundPickup = false;

            foreach (Collider col in colliders)
            {
                Rigidbody targetRb = col.attachedRigidbody;
                if (targetRb != null && targetRb != rb)
                {
                    foundPickup = true;
                    break;
                }
            }

            if (foundPickup)
            {
                pickupText.gameObject.SetActive(true);
                pickupText.text = $"Press {pickupKey} to pick up";
            }
            else
            {
                pickupText.gameObject.SetActive(false);
            }
        }
        else
        {
            pickupText.gameObject.SetActive(true);
            pickupText.text = $"Press {pickupKey} to drop";
        }
    }

    void PickUpObject(Rigidbody obj)
    {
        heldObject = obj;
        heldObject.useGravity = false;
        heldObject.isKinematic = true;
        heldObject.GetComponent<BoxCollider>().isTrigger = true;
        heldObject.transform.position = holdPoint.position;
        heldObject.transform.parent = holdPoint;
        heldObject.transform.rotation = holdPoint.rotation;
    }

    void DropObject()
    {
        heldObject.useGravity = true;
        heldObject.isKinematic = false;
        heldObject.GetComponent<BoxCollider>().isTrigger = false;
        heldObject.transform.parent = null;
        heldObject = null;
    }
}