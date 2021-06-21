using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour,IEndDragHandler,IDragHandler
{

    Vector2 current;            //현재 볼의 위치
            
    Vector2 first;              //초기 볼의 위치

    private float Radius;       //볼이 움직일 뱐경

    private Image currentRect;  //볼본인

    public Transform Player;    //플레이어

    private bool Moved = false; //플레이어 움직일 플래그

    private float rot;

    private float moveSpeed;
    public Image PreController;
    float d;
    
    void Start()
    {
        PreController = transform.parent.GetComponent<Image>();
        currentRect = GetComponent<Image>();
        Radius = 100;
        first = transform.position;
        //first = PreController.rectTransform.anchoredPosition;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Moved = true;                                           //드래그 하면 움직인다. 

        current = Input.mousePosition;                          //현재위치 동기화 

        moveSpeed = Vector2.Distance(current, first);
        Vector2 current2 = PreController.rectTransform.anchoredPosition;
        Debug.Log(current2);
        Vector2 dir = (current - first).normalized;             //볼이 어디로 움직엿는지 방향 채크
        
        rot = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;        //볼이 움직인 방향을 Atan2 로 구한다움에 플레이어에 넣어준다.

        float d = Vector2.Distance(current, first);

        if (d > Radius)
        {
            currentRect.rectTransform.position = first + (dir * Radius);

            //PreController.rectTransform.position;
        }
        else
        {
            currentRect.rectTransform.position = current;
        }

    }
          
    public void OnEndDrag(PointerEventData eventData)
    {
        currentRect.rectTransform.position = first;
        
        Moved = false;
    }

    void Update()
    {
        if (Moved)
        {
            Player.eulerAngles = new Vector3(0, 0, -rot);
            Player.Translate(Vector2.up * moveSpeed*0.005f * Time.deltaTime);
        }
    }


}
