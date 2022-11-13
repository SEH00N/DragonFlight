using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] float activeDelay = 0.5f;
    public float ActiveDelay => activeDelay;

    public abstract void ActiveWeapon();
}
