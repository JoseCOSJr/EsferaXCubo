using UnityEngine;

public class movement : MonoBehaviour
{
    private Rigidbody2D body;
    private Vector2 velocity = Vector2.zero;
    [SerializeField]
    private float speed = 3f;
    private attributes atb;
    private controllAnimations controllAnima; 

    // Start is called before the first frame update
    void Start()
    {
        controllAnima = GetComponent<controllAnimations>();
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

            if (dire.y * dire.y > dire.x *dire.x)
            {
                if (dire.y > 0f)
                {
                    controllAnima.SetMovement(true, controllAnimations.direction.up);
                }
                else
                {
                    controllAnima.SetMovement(true, controllAnimations.direction.down);
                }
            }
            else
            {
                if (dire.x > 0f)
                {
                    controllAnima.SetMovement(true, controllAnimations.direction.right);
                }
                else
                {
                    controllAnima.SetMovement(true, controllAnimations.direction.left);
                }
            }
        }
        else
        {
            controllAnima.SetMovement(false, controllAnima.GetDirection());
            body.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    /*public void TurnTo(float ang)
    {
        if (!atb.InHitStun())
            transform.eulerAngles = Vector3.forward * ang;
    }*/

    public void AddForce(Vector2 force)
    {
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
        body.AddForce(force * body.mass, ForceMode2D.Impulse);
    }

    public bool InMoviment()
    {
        return body.velocity.sqrMagnitude != 0f;
    }
}
