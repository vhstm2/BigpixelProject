using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SponerManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject FullObject;

    public int FullCount = 30;

    public List<GameObject> fullList = new List<GameObject>();

    public Transform[] sponerList;


    public void Reset()
    {
        for (int i = 0; i < FullCount; i++)
        {
            GameObject obj = Instantiate(FullObject, transform);
            obj.transform.position = transform.position;
            obj.SetActive(false);
            fullList.Add(obj);
        }
    }
    public GameObject GetObj()
    {
        for (int i = 0; i < fullList.Count; i++)
        {
            if (!fullList[i].activeSelf)
            {
                return fullList[i];
            }
        }
        return null;
    }

    void Start()
    {
        Reset();
    }
}
