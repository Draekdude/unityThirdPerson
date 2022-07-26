using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider myCollider;
    private List<Collider> _alreadyCollidedWith = new List<Collider>();
    private int _damage;
    private float _knockBack;

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
        if (other.TryGetComponent(out Health health))
        {
            health.DealDamage(_damage);
        }
        if (other.TryGetComponent(out ForceReceiver forceReceiver))
        {
            var singleDirection = (other.transform.position - myCollider.transform.position).normalized;
            forceReceiver.AddForce(singleDirection * _knockBack);
        }
    }

    public void SetAttack(int damage, float knockBack)
    {
        _damage = damage;
        _knockBack = knockBack;
    }
}
