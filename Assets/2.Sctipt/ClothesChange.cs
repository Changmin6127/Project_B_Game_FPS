using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class ClothesChange : MonoBehaviour  //Data Field
{
    private bool isChanging = false;
    private ClothesDissolveItem currentClothes;
    private ClothesDissolveItem prevClothes;
    private RendererDissolveHide dissolveHide;
    private RendererDissolveUnhide dissolveUnhide;

    [SerializeField] private string normalClothes;
    [SerializeField] private string uniformClothes;
    [SerializeField] private ClothesDissolveItem[] clothesItem;

    [Space(20)]
    [SerializeField] private string startId;
    [SerializeField] private UnityEvent changeStartActive;
    [SerializeField] private UnityEvent changeFinishActive;

    [SerializeField] private UnityEvent normalEvent;
    [SerializeField] private UnityEvent uniformEvent;
}

public partial class ClothesChange : MonoBehaviour  //Function Field
{
    private void Start()
    {
        dissolveHide = GetComponent<RendererDissolveHide>();
        dissolveUnhide = GetComponent<RendererDissolveUnhide>();

        for (int index = 0; index < clothesItem.Length; index++)
        {
            if (startId == clothesItem[index].id)
            {
                currentClothes = clothesItem[index];
                break;
            }
        }
    }

    public void ChangeActive(string _id)
    {
        if (isChanging || currentClothes.id == _id)
            return;

        prevClothes = null;
        for (int index = 0; index < clothesItem.Length; index++)
        {
            if (_id == clothesItem[index].id)
            {
                prevClothes = clothesItem[index];
                break;
            }
        }

        if (prevClothes == null)
            return;

        isChanging = true;

        dissolveHide.Active(currentClothes);
        changeStartActive?.Invoke();

        if (_id == normalClothes)
            normalEvent?.Invoke();
        if (_id == uniformClothes)
            uniformEvent?.Invoke();
    }

    public void PrevActive()
    {
        dissolveUnhide.Active(prevClothes);
    }

    public void ChangeFinish()
    {
        isChanging = false;
        currentClothes = prevClothes;
        changeFinishActive?.Invoke();
    }
}

