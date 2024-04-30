using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class RendererDissolveHide : MonoBehaviour //Data Field
{
    private bool isPerformace = false;

    private ClothesDissolveItem currentItem;
    private List<Material[]> limMaterials = new List<Material[]>();
    private List<Material[]> dissolveMaterials = new List<Material[]>();

    private float startMathf;
    private float finishMathf;

    //private Color startColor;
    //private Color finishColor;
    private float startIntensity;
    private float finishIntensity;

    private bool isLim;
    private float limDeltaTime = 0;

    private bool isDissolve;
    private float dissolveDeltaTime = 0;

    [SerializeField] PerformanceTimeAnimationCurve limOption;
    [SerializeField] PerformanceTimeAnimationCurve dissolveOption;

    [SerializeField] private UnityEvent activeEvent;
    [SerializeField] private UnityEvent limFinishEvent;
    [SerializeField] private UnityEvent dissolveFinishEvent;
}

public partial class RendererDissolveHide : MonoBehaviour //Unity Function Field
{
    private void Update()
    {
        LimProgress();
        DissolveProgress();
    }
}

public partial class RendererDissolveHide : MonoBehaviour //Property Function Field
{
    public void Active(ClothesDissolveItem _dissolveItem)
    {
        if (isPerformace)
            return;

        activeEvent?.Invoke();
        _dissolveItem.startEvent?.Invoke();

        isPerformace = true;
        currentItem = _dissolveItem;

        LimInitialize();
        DissolveInitialize();

        LimActive();
    }

    private void Finish()
    {
        isPerformace = false;
        dissolveFinishEvent?.Invoke();
        currentItem.finishEvent.Invoke();
    }

    private void LimInitialize()
    {
        limMaterials.Clear();

        for (int index = 0; index < currentItem.defaultClothesRenderer.Length; index++)
        {
            limMaterials.Add(currentItem.defaultClothesRenderer[index].materials);

            //foreach (var _item in limMaterials[index])
            //    _item.SetInt("_UseEmission", 1);
        }
    }

    private void DissolveInitialize()
    {
        dissolveMaterials.Clear();

        for (int index = 0; index < currentItem.dissolveClothesRenderer.Length; index++)
        {
            dissolveMaterials.Add(currentItem.dissolveClothesRenderer[index].materials);

            foreach (var _item in dissolveMaterials[index])
                _item.SetFloat("_DissolveAmount", 0);
        }
    }
}

public partial class RendererDissolveHide : MonoBehaviour //Performance Function Field
{
    private void LimActive()
    {
        startMathf = 0;
        finishMathf = 1;

        startIntensity = 0;
        finishIntensity = 1;
        //startColor = currentItem.normalColor;
        //finishColor = currentItem.limColor;
        limDeltaTime = 0;
        isLim = true;
    }

    private void LimProgress()
    {
        if (isLim)
        {
            limDeltaTime += Time.deltaTime * limOption.speed;

            for (int index = 0; index < limMaterials.Count; index++)
            {
                for (int index2 = 0; index2 < limMaterials[index].Length; index2++)
                {
                    limMaterials[index][index2].SetFloat("_MCapIntensity", Mathf.Lerp(startIntensity, finishIntensity, limOption.animationCurve.Evaluate(limDeltaTime)));
                    //limMaterials[index][index2].SetColor("_EmissionColor", Color.Lerp(startColor, finishColor, limOption.animationCurve.Evaluate(limDeltaTime)));
                }
            }

            if (limDeltaTime > 1)
            {
                isLim = false;
                for (int index = 0; index < currentItem.defaultClothesParentObject.Length; index++)
                    currentItem.defaultClothesParentObject[index].SetActive(false);
                for (int index = 0; index < currentItem.dissolveClothesParentObject.Length; index++)
                    currentItem.dissolveClothesParentObject[index].SetActive(true);
                DissolveActive();
                limFinishEvent?.Invoke();
            }
        }
    }

    private void DissolveActive()
    {
        startMathf = 0;
        finishMathf = 1;
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
                    dissolveMaterials[index][index2].SetFloat("_DissolveAmount", Mathf.Lerp(startMathf, finishMathf, dissolveOption.animationCurve.Evaluate(dissolveDeltaTime)));
                }
            }

            if (dissolveDeltaTime > 1)
            {
                isDissolve = false;
                for (int index = 0; index < currentItem.dissolveClothesParentObject.Length; index++)
                    currentItem.dissolveClothesParentObject[index].SetActive(false);
                Finish();
            }
        }
    }
}

[System.Serializable]
public class ClothesDissolveItem
{
    public string id;
    public Color normalColor;
    public Color limColor;

    [Space(10)]
    public GameObject[] defaultClothesParentObject;
    public Renderer[] defaultClothesRenderer;

    [Space(10)]
    public GameObject[] dissolveClothesParentObject;
    public Renderer[] dissolveClothesRenderer;

    public UnityEvent startEvent;
    public UnityEvent finishEvent;
}

[System.Serializable]
public struct PerformanceTimeAnimationCurve
{
    public float speed;
    public AnimationCurve animationCurve;
}