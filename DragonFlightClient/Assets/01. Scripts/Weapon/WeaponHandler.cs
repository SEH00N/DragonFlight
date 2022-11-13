using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] Weapon currentWeapon = null;
    [SerializeField] KeyCode activeKey = KeyCode.Mouse0;
    private float timer = 0f;

    private void Update()
    {
        if(currentWeapon.ActiveDelay >= timer)
            timer += Time.deltaTime;

        if(Input.GetKeyDown(activeKey))
            if(currentWeapon.ActiveDelay <= timer)
                currentWeapon.ActiveWeapon();
    }
}
