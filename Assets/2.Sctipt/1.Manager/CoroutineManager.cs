using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CoroutineManager : MonoBehaviour   //Data Field
{
    private WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
    private readonly Dictionary<float, WaitForSeconds> timeInterval = new Dictionary<float, WaitForSeconds>(new FloatComparer());
}

public partial class CoroutineManager : MonoBehaviour   //Property Function Field
{
    public void WaitForSecond(float time)
    {
        StartCoroutine(WaitForSecondsCoroutine(time));
    }
    public void WaitForSecond(System.Action receiveFunction, float time)
    {
        StartCoroutine(WaitForSecondsCoroutine(receiveFunction, time));
    }
    public void WaitForEndOfFrame(System.Action receiveFunction)
    {
        StartCoroutine(WaitForEndOfFrameCoroutine(receiveFunction));
    }
}

public partial class CoroutineManager : MonoBehaviour   //Coroutine Function Field
{
    IEnumerator WaitForEndOfFrameCoroutine(System.Action receiveFunction)
    {
        yield return waitForEndOfFrame;
        yield return waitForEndOfFrame;
        receiveFunction?.Invoke();
    }
    IEnumerator WaitForSecondsCoroutine(System.Action receiveFunction, float time)
    {
        yield return WaitForSeconds(time);
        receiveFunction?.Invoke();
    }

    IEnumerator WaitForSecondsCoroutine(float time)
    {
        yield return WaitForSeconds(time);
    }
}

public partial class CoroutineManager : MonoBehaviour   //Assistance Function Field
{
    public WaitForSeconds WaitForSeconds(float time)
    {
        WaitForSeconds waitForSeconds;

        if (timeInterval.TryGetValue(time, out waitForSeconds) == false)
            timeInterval.Add(time, waitForSeconds = new WaitForSeconds(time));

        return waitForSeconds;
    }
}

public class FloatComparer : IEqualityComparer<float>
{
    bool IEqualityComparer<float>.Equals(float x, float y)
    {
        return x == y;
    }
    int IEqualityComparer<float>.GetHashCode(float obj)
    {
        return obj.GetHashCode();
    }
}