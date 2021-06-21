using UnityEngine;

public class Missile : MonoBehaviour
{
    private bool b_fire = false;

    public ThrowSimulator pomul;

    public Transform resetPos;
    public Transform parent;
    public Transform height;
    public Transform[] endpos;

    public void Fire()
    {
        b_fire = true;
        transform.SetParent(null);
        var pos = transform.position + transform.forward * Random.Range(10, 12);

        pomul.Shoot(transform, transform.position, endpos[Random.Range(0, endpos.Length)].position, pomul.g, pos.z, Oncomplate, missileD);
        //미사일 발사
    }

    private void missileD()
    {
        b_fire = false;
        transform.position = resetPos.position;
        transform.rotation = resetPos.rotation;
    }

    private void Oncomplate()
    {
        b_fire = false;
        gameObject.SetActive(false);
        transform.SetParent(parent);
    }
}