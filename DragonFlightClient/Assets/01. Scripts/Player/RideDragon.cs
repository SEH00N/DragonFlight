using UnityEngine;

public class RideDragon : MonoBehaviour
{
    [Header("Ray")]
    [SerializeField] Transform rayPosition = null;
    [SerializeField] float rayDistance = 10f;

    [Header("Origin")]
    [SerializeField] Vector3 cameraOriginRotation = new Vector3();

    private PlayerMovement playerMovement = null;
    private DragonMovement currentDragon = null;

    private TextPrefab currentRideText = null;
    private bool isRide = false;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(!isRide)
        {
            if (TargetingDragon(out currentDragon))
            {
                ShowNoitceText();
                if(Input.GetKeyDown(KeyCode.E))
                    DoRideOn();
            }
            else if(currentRideText != null)
                HideNoticeText();
        }
        else if (currentDragon != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
                DoRideOff();

            if(currentRideText != null)
                HideNoticeText();
        }
    }

    private void HideNoticeText()
    {
        PoolManager.Instance.Push(currentRideText);
        currentRideText = null;
    }

    private void ShowNoitceText()
    {
        if(currentRideText == null) 
        {
            currentRideText = PoolManager.Instance.Pop("NoticeTextPrefab") as TextPrefab;
            currentRideText.Init("[E] : Ride", DEFINE.MainCanvas);
        }

        currentRideText.transform.position = DEFINE.MainCam.WorldToScreenPoint(currentDragon.playerRidePosition.position);
    }

    private bool TargetingDragon(out DragonMovement targetDragon)
    {
        targetDragon = null;
        // Debug.DrawRay(rayPosition.position, DEFINE.CmMainCam.transform.forward, Color.red, 1f);

        bool isDragon = Physics.Raycast(rayPosition.position, DEFINE.CmMainCam.transform.forward, out RaycastHit hit, rayDistance, DEFINE.DragonLayer);
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
        DEFINE.CmMainCam.transform.localRotation = Quaternion.Euler(cameraOriginRotation);
    }

    private void DoRideOff()
    {
        playerMovement.Active = true;
        currentDragon.Active = false;

        transform.SetParent(null);
        currentDragon = null;
        
        DEFINE.CmMainCam.Follow = playerMovement.cameraFollow;
        DEFINE.CmMainCam.transform.SetParent(playerMovement.cameraFollow);
        DEFINE.CmMainCam.transform.localRotation = Quaternion.Euler(cameraOriginRotation);

        isRide = false;
    }
}
