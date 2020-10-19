using UnityEngine;

public class progetile : MonoBehaviour
{
    [SerializeField]
    private string nameId = "";
    [SerializeField]
    [Range(5f, 20f)]
    private float velocity = 10f;
    private float timeDelta;
    private weaponInfs weaponInfs;
    protected Rigidbody2D body;
    private string tagOwner;

    public void Inocation(weaponInfs infs, Vector3 pos, Vector2 dire, string tag)
    {
        gameObject.SetActive(true);
        tagOwner = tag;
        weaponInfs = infs;
        timeDelta = infs.GetReach() / velocity;
        transform.position = pos;
        VelocityChange(dire * velocity);
    }

    protected virtual void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public string NameId()
    {
        return nameId;
    }

    private void FixedUpdate()
    {
        if (timeDelta > 0f)
        {
            timeDelta -= Time.fixedDeltaTime;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    protected void VelocityChange(Vector2 value)
    {
        body.velocity = value;
        float angZ = Vector2.SignedAngle(transform.up, value);
        transform.eulerAngles = Vector3.forward * angZ;
    }


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        attributes atb = collision.GetComponentInParent<attributes>();
        if (atb && !atb.CompareTag(tagOwner))
            DamgeAplication(atb, 1f, Vector2.zero);

        gameObject.SetActive(false);
    }

    public virtual void DamgeAplication(attributes atb, float multiply, Vector2 force)
    {
        atb.AddHp((int)(-weaponInfs.GetDamage()*multiply), Vector2.zero);
    }
}
