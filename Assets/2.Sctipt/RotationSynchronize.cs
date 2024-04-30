using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSynchronize : MonoBehaviour
{
    [SerializeField] private Transform target;

    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, 0.5f);
    }
}
