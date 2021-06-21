using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerSponer : MonoBehaviour
{
    public GameObject[] fulllList_Light;
  
    public Transform[] sponerList;
    public List<GameObject> LazerList = new List<GameObject>();

    //public void Reset()
    //{
    //    GameObject ob = new GameObject("Light");
    //    for (int i = 0; i < fullCount; i++)
    //    {
    //        GameObject obj = Instantiate(fullObject[0], ob.transform);
    //        obj.name = "Light_" + i;
    //        obj.transform.position = ob.transform.position;
    //        obj.SetActive(false);
    //        fulllList_Light.Add(obj);
    //    }

    //    GameObject ob2 = new GameObject("Lazer");
    //    for (int i = 0; i < fullCount; i++)
    //    {
    //        GameObject obj = Instantiate(fullObject[1], ob2.transform);
    //        obj.name = "Lazer_" + i;
    //        obj.transform.position = ob2.transform.position;
    //        obj.SetActive(false);
    //        fulllList_Lazer.Add(obj);
    //    }

    //}

    public int GetObj_Light()
    {
        for (int i = 0; i < fulllList_Light.Length; i++)
        {
            if (fulllList_Light[i].activeSelf == false)
            {
                return i;
            }
        }
        return 0;
    }

  

   
}

