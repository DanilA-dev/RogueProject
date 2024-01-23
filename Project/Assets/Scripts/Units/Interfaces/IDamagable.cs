using UnityEngine;

public interface IDamagable
{
    public GameObject GameObject {get;}
    public bool CanBeDamaged {get;}
    public void Damage(float damageAmount, object damageDelear);
}
