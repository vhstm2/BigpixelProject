using UnityEngine;

public class blade : MonoBehaviour
{
    public float rot;

    public void Onupdate()
    {
        var collider = Physics.OverlapCapsule
         (
              transform.position,
             transform.position - transform.forward * 1.3f, 0.18f, 1 << 8
         );

        if (collider.Length > 0)
        {
            foreach (var col in collider)
            {
                if (col.CompareTag("Enemy")) col.gameObject.SetActive(false);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - transform.forward * 1.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
        }
    }
}