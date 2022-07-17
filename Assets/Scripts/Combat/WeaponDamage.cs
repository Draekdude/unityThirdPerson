using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider myCollider;
    private List<Collider> _alreadyCollidedWith = new List<Collider>();
    private int _damage;

    private void OnEnable()
    {
        _alreadyCollidedWith.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag(gameObject.tag)) { return; }
        if (other == myCollider) { return; }
        if (_alreadyCollidedWith.Contains(other)) { return; }
        _alreadyCollidedWith.Add(other);
        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(_damage);
        }
    }

    public void SetAttack(int damage)
    {
        _damage = damage;
    }
}
