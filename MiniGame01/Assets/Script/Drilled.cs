using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drilled : MonoBehaviour
{
    // Start is called before the first frame update


    public void DrilledSkill()
    {
        transform.rotation *= Quaternion.Euler(Vector3.forward * Time.deltaTime * 300.0f);
    }
}
