using UnityEngine;

public class damageArea : MonoBehaviour
{
    [SerializeField]
    private float forceImpulse = 20f;
    private progetile progetile;

    public void Invocation(progetile progetile, Vector3 pos)
    {
        this.progetile = progetile;
        transform.position = pos;
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        attributes atb = collision.GetComponentInParent<attributes>();
        Vector2 force = atb.transform.position;
        force.x -= transform.position.x;
        force.y -= transform.position.y;
        force = force.normalized * forceImpulse;
        progetile.DamgeAplication(atb, 3f, force);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
