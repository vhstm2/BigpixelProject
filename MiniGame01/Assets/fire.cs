using DG.Tweening;
using UnityEngine;

public class fire : MonoBehaviour
{
    public PlayerCtrl playerdir;

    private Vector3 dir;

   
    private float fireTimer = 16f;

    [SerializeField]
    private float firedeltaTimer;

    public int dirNumber;

    
    public fire_info fireinfo = fire_info.bullet;

    private void OnEnable()
    {
        //dir = playerdir.dir;
        ////각도조정
        //transform.localScale = Vector3.zero;

        //var dn = dir.normalized;
        //dn.y = 0;
        //var rot = Quaternion.LookRotation(dn);
        //transform.rotation = rot;

        transform.rotation = Quaternion.Euler(0, dirNumber, 0);

        transform.DOScale(Vector3.one * 2f, 0.4f);
    }

    private Vector3 toneidov3 = Vector3.zero;

    private float radius = 0.01f;
    private float radiusTimer = 0;
    private void Update()
    {
        if (!gameObject.activeSelf) return;

        if (fireinfo == fire_info.bullet)
        {
            firedeltaTimer += Time.deltaTime;

            transform.Translate(Vector3.forward * Time.deltaTime * 20f);

            if (firedeltaTimer >= fireTimer)
            {
                gameObject.SetActive(false);
                firedeltaTimer = 0;
            }
        }
        else if (fireinfo == fire_info.tonaido)
        {
            //회전 
            firedeltaTimer += Time.deltaTime;

            if ( (radiusTimer += Time.deltaTime) >= 0.5f) 
            {
                radius += 0.005f;
                radiusTimer = 0;
            }

            float x = Mathf.Cos(180.0f + firedeltaTimer) * (radius);
            float z = Mathf.Sin(180.0f +  firedeltaTimer) *  (radius);

            toneidov3 = 
                new Vector3
                ( 
                    (x+transform.position.x) 
                    , 0, 
                    (z + transform.position.z) 
                );

            transform.position = toneidov3;

            transform.right = toneidov3;

            if (firedeltaTimer >= fireTimer)
            {
                gameObject.SetActive(false);
                firedeltaTimer = 0;
                radius = 0.01f;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
    }
}