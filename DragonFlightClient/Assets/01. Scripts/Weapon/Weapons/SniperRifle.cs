using UnityEngine;

public class SniperRifle : Weapon
{
    [SerializeField] KeyCode zoomKey = KeyCode.Mouse1;
    [SerializeField] float damage = 10f;

    private bool onZoom = false;

    public override void ActiveWeapon()
    {
        if(!TryTargeting(out Collider enemy))
            return;

        if(enemy.transform.root.TryGetComponent<IDamageable>(out IDamageable id))
            id.OnDamage(damage);
    }

    private void Update()
    {
        if(Input.GetKeyDown(zoomKey))
            Zoom();
    }

    private void Zoom()
    {
        if(onZoom)
        {
            
        }
    }

    //해줘
    private bool TryTargeting(out Collider other)
    {
        

        other = new Collider();
        return true;
    }

    //해줘
    private void FireEffect(Vector3 pos)
    {
        //빵야
    }
}
