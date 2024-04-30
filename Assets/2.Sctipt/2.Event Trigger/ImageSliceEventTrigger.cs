using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public partial class ImageSliceEventTrigger : MonoBehaviour
{
    private bool isActive = false;
    private float deltaTime = 0;

    [SerializeField] private Image target;
    [SerializeField] private float speed = 1;
    [SerializeField] private bool isStart = false;
    [SerializeField] private UnityEvent finishEvent;
}

public partial class ImageSliceEventTrigger : MonoBehaviour
{
    private void Start()
    {
        if (isStart)
        {
            Active();
        }
    }
    public void Active()
    {
        target.fillAmount = 0;
        deltaTime = 0;
        isActive = true;
    }

    private void Update()
    {
        if (isActive)
        {
            deltaTime += Time.deltaTime * speed;

            target.fillAmount = deltaTime / 1;

            if(deltaTime > 1)
            {
                isActive = false;
                finishEvent?.Invoke();
            }
        }
    }
}