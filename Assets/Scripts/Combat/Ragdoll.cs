using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController controller;

    private Collider[] _colliders;
    private Rigidbody[] _rigidbodies;

    void Start()
    {
        _colliders = GetComponentsInChildren<Collider>(true);
        _rigidbodies = GetComponentsInChildren<Rigidbody>(true);
        ToggleRagdoll(false);
    }

    public void ToggleRagdoll(bool isRagdoll)
    {
        //foreach (var collider in _colliders)
        //{
        //    if (collider.CompareTag("Ragdoll"))
        //    {
        //        collider.enabled = isRagdoll;
        //    }
        //}
        _colliders.Where(c => c.CompareTag("Ragdoll"))
            .ToList()
            .ForEach(x => x.enabled = isRagdoll);
        //foreach (var rigidbody in _rigidbodies)
        //{
        //    if(rigidbody.CompareTag("Ragdoll"))
        //    {
        //        rigidbody.isKinematic = !isRagdoll;
        //        rigidbody.useGravity = isRagdoll;
        //    }
        //}
        _rigidbodies.Where(c => c.CompareTag("Ragdoll"))
            .ToList()
            .ForEach(x =>
            {
                x.isKinematic = !isRagdoll;
                x.useGravity = isRagdoll;
            });
        controller.enabled = !isRagdoll;
        animator.enabled = !isRagdoll;
    }
}
