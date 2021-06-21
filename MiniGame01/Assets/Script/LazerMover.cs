using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerMover : MonoBehaviour
{
    public Vector2 po = new Vector2();
    private float min = -0.5f;
    private float max = 1.5f;

    private PlayerCtrl player;
    private ITEM_SKILL item_Skill;

    public enum LazerScale { A, B, C, End }

    public sunActive[] BillenOb;

    
    public void Awake()
    {
        player = FindObjectOfType<PlayerCtrl>();
        item_Skill = FindObjectOfType<ITEM_SKILL>();
    }

    public void OnEnable()
    {
        BillenChangeOf();
    }

    private void BillenChangeOf()
    {
        BillenOb[UserDataManager.instance.Player_Eqip.StageNumber].gameObject.SetActive(true);
        for (int i = 0; i < BillenOb.Length; i++)
        {
            if (i == UserDataManager.instance.Player_Eqip.StageNumber)
                continue;
            else if (BillenOb[i].gameObject.activeSelf)
                BillenOb[i].gameObject.SetActive(false);
        }
        BillenOb[UserDataManager.instance.Player_Eqip.StageNumber].enabledFuns();

    }

    private float ActiveOffTimer;

    // Update is called once per frame
    public void Update()
    {
        if (gameObject.activeSelf)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 20 * item_Skill.slow.SlowSpeed
                * UserDataManager.instance.BillenSpeed);

            po = Camera.main.WorldToViewportPoint(transform.position);

            if (po.x <= min || po.x >= max || po.y < min || po.y > max)
            {
                ActiveOffTimer += Time.deltaTime;
            }

            if (ActiveOffTimer >= 3.0f)
            {
                ActiveOffTimer = 0;
                gameObject.SetActive(false);
            }
        }
    }

    public void OnTriggerStay(Collider coll)
    {
        if (coll.CompareTag("Enemy"))
        {
            coll.transform.parent.gameObject.SetActive(false);
        }
        //if (coll.CompareTag("Player"))
        //{
        //    player.gameManager.GameSceneState = GameManager.GameSceneState.GameEnd;
        //}
    }
}