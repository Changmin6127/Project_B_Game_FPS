using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class RendererDissolveUnhide : MonoBehaviour //Data Field
{
    private bool isPerformace = false;

    private ClothesDissolveItem currentItem;
    private List<Material[]> dissolveMaterials = new List<Material[]>();
    private List<Material[]> unlimMaterials = new List<Material[]>();

    private float startMathf;
    private float finishMathf;

    //private Color startColor;
    //private Color finishColor;
    private float startIntensity;
    private float finishIntensity;

    private bool isDissolve;
    private float dissolveDeltaTime = 0;

    private bool isUnlim;
    private float unlimDeltaTime = 0;

    [SerializeField] PerformanceTimeAnimationCurve dissolveOption;
    [SerializeField] PerformanceTimeAnimationCurve unlimOption;
    [SerializeField] private UnityEvent activeEvent;
    [SerializeField] private UnityEvent dissolveFinishEvent;
    [SerializeField] private UnityEvent unlimFinishEvent;
}

public partial class RendererDissolveUnhide : MonoBehaviour //Unity Function Field
{
    private void Update()
    {
        DissolveProgress();
        UnlimProgress();
    }
}

public partial class RendererDissolveUnhide : MonoBehaviour //Property Function Field
{
    public void Active(ClothesDissolveItem _dissolveItem)
    {
        if (isPerformace)
            return;

        activeEvent?.Invoke();
        _dissolveItem.startEvent?.Invoke();

        isPerformace = true;
        currentItem = _dissolveItem;

        DissolveInitialize();
        UnlimInitialize();

        DissolveActive();
    }

    private void Finish()
    {
        isPerformace = false;
        unlimFinishEvent?.Invoke();
        currentItem.finishEvent?.Invoke();
    }

    private void DissolveInitialize()
    {
        dissolveMaterials.Clear();

        for (int index = 0; index < currentItem.dissolveClothesRenderer.Length; index++)
        {
            dissolveMaterials.Add(currentItem.dissolveClothesRenderer[index].materials);

            foreach (var _item in dissolveMaterials[index])
                _item.SetFloat("_DissolveAmount", 1);
        }

        for (int index = 0; index < currentItem.dissolveClothesParentObject.Length; index++)
            currentItem.dissolveClothesParentObject[index].SetActive(true);
    }

    private void UnlimInitialize()
    {
        unlimMaterials.Clear();

        for (int index = 0; index < currentItem.defaultClothesRenderer.Length; index++)
        {
            unlimMaterials.Add(currentItem.defaultClothesRenderer[index].materials);

            //foreach (var _item in unlimMaterials[index])
            //{
            //    _item.SetInt("_UseEmission", 1);
            //    Color _color = _item.GetColor("_EmissionColor");
            //    _item.SetColor("_EmissionColor", new Color(_color.r, _color.g, _color.b, 1));
            //}
        }
    }
}

public partial class RendererDissolveUnhide : MonoBehaviour //Performance Function Field
{
    private void DissolveActive()
    {
        startMathf = 1;
        finishMathf = 0;
        dissolveDeltaTime = 0;
        isDissolve = true;
    }

    private void DissolveProgress()
    {
        if (isDissolve)
        {
            dissolveDeltaTime += Time.deltaTime * dissolveOption.speed;

            for (int index = 0; index < dissolveMaterials.Count; index++)
            {
                for (int index2 = 0; index2 < dissolveMaterials[index].Length; index2++)
                {
                    dissolveMaterials[index][index2].SetFloat("_DissolveAmount",Mathf.Lerp(startMathf, finishMathf, dissolveOption.animationCurve.Evaluate(dissolveDeltaTime)));
                }
            }

            if (dissolveDeltaTime > 1)
            {
                isDissolve = false;
                for (int index = 0; index < currentItem.dissolveClothesParentObject.Length; index++)
                    currentItem.dissolveClothesParentObject[index].SetActive(false);
                for (int index = 0; index < currentItem.defaultClothesParentObject.Length; index++)
                    currentItem.defaultClothesParentObject[index].SetActive(true);
                UnlimActive();
                dissolveFinishEvent?.Invoke();
            }
        }
    }

    private void UnlimActive()
    {
        startIntensity = 1;
        finishIntensity = 0;
        //startColor = currentItem.limColor;
        //finishColor = currentItem.normalColor;
        unlimDeltaTime = 0;
        isUnlim = true;
    }

    private void UnlimProgress()
    {
        if (isUnlim)
        {
            unlimDeltaTime += Time.deltaTime * unlimOption.speed;

            for (int index = 0; index < unlimMaterials.Count; index++)
            {
                for (int index2 = 0; index2 < unlimMaterials[index].Length; index2++)
                {
                    unlimMaterials[index][index2].SetFloat("_MCapIntensity", Mathf.Lerp(startIntensity, finishIntensity, unlimOption.animationCurve.Evaluate(unlimDeltaTime)));
                    //unlimMaterials[index][index2].SetColor("_EmissionColor", Color.Lerp(startColor, finishColor, unlimOption.animationCurve.Evaluate(unlimDeltaTime)));
                }
            }

            if (unlimDeltaTime > 1)
            {
                isUnlim = false;

                //for (int index = 0; index < unlimMaterials.Count; index++)
                //{
                //    for (int index2 = 0; index2 < unlimMaterials[index].Length; index2++)
                //    {
                //        unlimMaterials[index][index2].SetInt("_UseEmission", 0);
                //    }
                //}

                Finish();
            }
        }
    }
}