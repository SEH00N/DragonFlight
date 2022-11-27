public interface IDamageable
{
    public float MaxHp { get; }
    public float CurrentHp { get; set; }

    public void OnDamage(float damage);
}
