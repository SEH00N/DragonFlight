using UnityEngine;

public class RideDragon : MonoBehaviour
{
    [SerializeField] Transform rayPosition = null;
    [SerializeField] float rayDistance = 10f;

    private PlayerMovement playerMovement = null;
    private DragonMovement currentDragon = null;

    private bool isRide = false;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(!isRide)
            {
                if(TargetingDragon(out currentDragon))
                    DoRideOn();
            }
            else if(currentDragon != null)
                DoRideOff();
        }
    }

    private bool TargetingDragon(out DragonMovement targetDragon)
    {
        targetDragon = null;

        bool isDragon = Physics.Raycast(rayPosition.position, rayPosition.forward, out RaycastHit hit, rayDistance, DEFINE.DragonLayer);
        if(isDragon)
            isDragon &= hit.collider.transform.root.TryGetComponent<DragonMovement>(out targetDragon);

        return isDragon;
    }

    private void DoRideOn()
    {
        isRide = true;

        playerMovement.Active = false;
        currentDragon.Active = true;

        transform.position = currentDragon.playerRidePosition.position;
        transform.SetParent(currentDragon.playerRidePosition);
        transform.localRotation = Quaternion.Euler(Vector3.zero);

        DEFINE.CmMainCam.Follow = currentDragon.cameraFollow;
        DEFINE.CmMainCam.transform.SetParent(currentDragon.cameraFollow);
    }

    private void DoRideOff()
    {
        playerMovement.Active = true;
        currentDragon.Active = false;

        transform.SetParent(null);
        currentDragon = null;
        
        DEFINE.CmMainCam.Follow = playerMovement.cameraFollow;
        DEFINE.CmMainCam.transform.SetParent(playerMovement.cameraFollow);

        isRide = false;
    }
}
