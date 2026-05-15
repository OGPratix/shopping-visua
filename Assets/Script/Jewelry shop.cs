using UnityEngine;

public class Jewelryshop : MonoBehaviour
{
    [Header("Pickup Settings")]
    public float pickupRange = 3.0f;
    public KeyCode pickupKey = KeyCode.E;
    public LayerMask jewelryLayer;
    public Transform holdPosition;

    private Jewelry currentHeldItem = null;
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(pickupKey))
        {
            if (currentHeldItem == null)
                TryPickUp();
            else
                DropCurrentItem();
        }
    }

    void TryPickUp()
    {
        Ray ray = mainCam.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange, jewelryLayer))
        {
            Jewelry jewelry = hit.collider.GetComponent<Jewelry>();
            if (jewelry != null)
            {
                currentHeldItem = jewelry;
                jewelry.OnPickedUp();
                
                jewelry.transform.SetParent(holdPosition);
                jewelry.transform.localPosition = Vector3.zero;
                jewelry.transform.localRotation = Quaternion.identity;
                
                Debug.Log("Picked up: " + jewelry.jewelryName);
            }
        }
    }

    void DropCurrentItem()
    {
        if (currentHeldItem == null) return;
        
        currentHeldItem.transform.SetParent(null);
        currentHeldItem.OnDropped();
        Debug.Log("Dropped: " + currentHeldItem.jewelryName);
        currentHeldItem = null;
    }
}

