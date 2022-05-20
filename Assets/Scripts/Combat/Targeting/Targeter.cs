using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    public List<Target> targets = new List<Target>();

    private void OnTriggerEnter(Collider other) {
        if (!other.TryGetComponent<Target>(out Target newTarget)) {return;}
        targets.Add(newTarget);

    }

    private void OnTriggerExit(Collider other) {
        if (other.TryGetComponent<Target>(out Target currentTarget)) {return;}
        targets.Remove(currentTarget);
    }

}
