using System.Collections;
using UnityEngine;

// NOTE: This is not used because it is not working properly.

/// <summary>
/// Special coroutine that allows to return a result
/// </summary>
public class CoroutineWithData<T>
{

    private IEnumerator _target;
    public T result;
    public Coroutine Coroutine { get; private set; }

    public CoroutineWithData(MonoBehaviour owner_, IEnumerator target_)
    {
        _target = target_;
        Coroutine = owner_.StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        while (_target.MoveNext())
        {
            // TODO: ERROR when casting to T
            result = (T)_target.Current;
            yield return result;
        }

    }
}
