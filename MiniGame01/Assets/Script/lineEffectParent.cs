using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineEffectParent : MonoBehaviour
{

   

    private void OnEnable() 
    {
        
        transform.rotation = Quaternion.Euler(0,Random.Range(0.0f,359.0f),0);
    }
    
}
