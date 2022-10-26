using UnityEngine;

public class DragonHealth : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHp = 100f;
    public float MaxHp => MaxHp;

    private float currentHp = 0f;
    public float CurrentHp { get => currentHp; set => currentHp = value; 
    }

    public void OnDamage(float damage)
    {
        throw new System.NotImplementedException();
    }
}
