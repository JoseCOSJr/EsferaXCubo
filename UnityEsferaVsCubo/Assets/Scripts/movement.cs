using UnityEngine;

public class movement : MonoBehaviour
{
    private Rigidbody2D body;
    private Vector2 velocity = Vector2.zero;
    [SerializeField]
    private float speed = 3f;
    private attributes atb;

    // Start is called before the first frame update
    void Start()
    {
        atb = GetComponent<attributes>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!atb.InHitStun())
        {
            if (body.velocity != velocity)
                body.velocity = velocity;
        }

    }

    public void Move(Vector2 dire)
    {
        if (dire.sqrMagnitude > 0f)
        {
            velocity = dire * speed;
            if (body.constraints != RigidbodyConstraints2D.FreezeRotation)
            {
                body.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
        else
        {
            body.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }


    public void TurnTo(float ang)
    {
        if (!atb.InHitStun())
            transform.eulerAngles = Vector3.forward * ang;
    }

    public void AddForce(Vector2 force)
    {
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
        body.AddForce(force * body.mass, ForceMode2D.Impulse);
    }
}
