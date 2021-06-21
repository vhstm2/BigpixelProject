using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchParent : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {
        StartCoroutine(commot());
    }

    public virtual IEnumerator middleBillen()
    {
        yield return null;
    }

    public virtual IEnumerator commot()
    {
        yield return null;
    }

    public virtual void Endcommot()
    {
    }
}