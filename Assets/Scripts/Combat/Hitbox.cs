using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private float damage;

    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }

    private void OnTriggerStay(Collider other)
    {
        CharacterStats stats = other.GetComponentInParent<CharacterStats>();

        stats.TakeDamage(damage);
    }

    private void OnDisable()
    {
        damage = 0f;
    }
}
