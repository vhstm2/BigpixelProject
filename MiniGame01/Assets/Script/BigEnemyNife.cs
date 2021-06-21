using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemyNife : BigEnemy
{
    public OneNife[] bigNifes;
    public OneNife[] bigNifes2;

    public override void stating()
    {
        base.stating();

        StartCoroutine(nifes_stating());
    }


    public override void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            stating();
        }
    }


    private IEnumerator nifes_stating()
    {
        yield return null;

        #region 패턴1

        for(int i = 0 ; i < bigNifes.Length ; i++)
        {

            bigNifes[i].gameObject.SetActive(true);
            bigNifes[i].nifeStating();
            yield return new WaitForSeconds(0.3f);
        }
        #endregion

        yield return new WaitForSeconds(5);

        #region 패턴2

        for(int i = 0 ; i < bigNifes2.Length ; i++)
        {
            bigNifes2[i].gameObject.SetActive(true);
            bigNifes2[i].nifeStating();
            yield return new WaitForSeconds(0.3f);
        }
        #endregion

        yield return new WaitForSeconds(5);
        
        StartCoroutine(End());
         
        
    }


    public override IEnumerator End()
    {
        //yield return null;
        //Debug.Log("End 다음몹나와라");
        yield return base.End();
    }

}
