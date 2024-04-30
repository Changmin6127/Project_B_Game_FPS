using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHat : MonoBehaviour
{
    [SerializeField] private Transform targetGuideTransform;
    [SerializeField] private Transform headGuideTransform;
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float rotationSpeed = 5.0f;
    [SerializeField] private bool isStartNotParent;
    private void Start()
    {
        if (isStartNotParent)
            transform.parent = null;
    }
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetGuideTransform.position, Time.deltaTime * moveSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetGuideTransform.rotation, Time.deltaTime * rotationSpeed);
        //transform.LookAt(headGuideTransform);
    }
}
