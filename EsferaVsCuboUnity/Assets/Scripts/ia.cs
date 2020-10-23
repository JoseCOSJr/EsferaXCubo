using UnityEngine;
using System.Collections;
using UnityEngine.Assertions.Must;

public class ia : MonoBehaviour
{
    private enum statesIa { awareness, chase , shooting }
    private statesIa stateNow = statesIa.awareness;
    private float timeAction = 0.1f;
    private attributes atb, atbTarget = null;
    private Vector2 targetPosition = Vector2.zero;
    private float distTarget;
    private bool moveTarget = false;
    private Renderer renderer;
    private Collider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        atb = GetComponent<attributes>();
        renderer = GetComponent<Renderer>();
        coll = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        stateNow = statesIa.awareness;
        atbTarget = null;
        StartCoroutine(IAAtive());
    }

    IEnumerator IAAtive()
    {
        while (true)
        {
            if(atb && !atb.IsDead())
            {
                if(stateNow == statesIa.awareness)
                {
                    if (!moveTarget)
                    {
                        //Dando direção aletoria para patrulhar
                        float walkDist = Random.Range(3f, 9f), angZ = Random.Range(0f, 360f);
                        Vector2 dire = Quaternion.Euler(Vector3.forward * angZ) * Vector2.up;
                        //Debug.Log(dire);
                        targetPosition = transform.position;
                        targetPosition += dire * walkDist;
                        distTarget = 1f;
                        moveTarget = true;
                        //Olhar apara direção do movimento
                        atb.GetMovement().TurnTo(Vector2.SignedAngle(Vector2.up, dire));
                    }

                    //Visão da IA
                    //Area da que a camera pega
                    Camera cam = Camera.main;
                    Vector2 size;
                    size.y = 2f * cam.orthographicSize;
                    size.x = size.y * cam.aspect;
                    Vector2 point1 = transform.position, point2 = point1 + size / 2f;
                    point1 -= size / 2f;

                    Collider2D[] colls = Physics2D.OverlapAreaAll(point1, point2, repository.GetLayerMaskChars());
                    //Todos os personagens na area
                    for(int i = 0; i < colls.Length; i++)
                    {
                        Collider2D c = colls[i];
                        if (!c.CompareTag(tag))
                        {
                            //Angulo de visão
                            Vector2 delta = c.transform.position - transform.position;
                            float angZ = Vector2.SignedAngle(transform.up, delta);

                            if (angZ * angZ <= 90 * 90)
                            {
                                //Dizendo que é o alvo
                                attributes atbX = c.GetComponent<attributes>();

                                if (!atbX.IsDead())
                                {
                                    atbTarget = atbX;
                                    stateNow = statesIa.chase;
                                }
                            }
                        }
                    }
                }
                else if(stateNow == statesIa.chase)
                {
                    actions act = atb.GetActions();
                    act.StopFire();

                    if (atbTarget.IsDead())
                    {
                        atbTarget = null;
                        stateNow = statesIa.awareness;
                    }
                    else
                    {

                        Vector2 delta = atbTarget.transform.position - transform.position;
                        if (delta.magnitude < act.ReachNow() && renderer.isVisible)
                        {
                            stateNow = statesIa.shooting;
                            moveTarget = false;
                        }
                        else
                        {
                            targetPosition = atbTarget.transform.position;
                            Vector2 dire = targetPosition;
                            dire.x -= transform.position.x;
                            dire.y -= transform.position.y;
                            atb.GetMovement().TurnTo(Vector2.SignedAngle(Vector2.up, dire));
                            distTarget = 2f;
                            moveTarget = true;
                        }
                    }
                }
                else if(stateNow == statesIa.shooting)
                {
                    Vector2 delta = atbTarget.transform.position - transform.position;
                    actions act = atb.GetActions();

                    if (delta.magnitude < act.ReachNow() && renderer.isVisible && !atbTarget.IsDead())
                    {
                        float angZNeed = Vector2.SignedAngle(Vector2.up, delta), angZNow = transform.eulerAngles.z;
                        if (angZNeed < 0f)
                            angZNeed += 360f;
                        if (angZNow < 0f)
                            angZNow += 360f;

                        float angZDif = angZNeed - angZNow;

                        if (angZDif*angZDif > 400)
                        {
                            atb.GetMovement().TurnTo(angZNeed);
                        }
                        else
                        {
                            act.Fire();
                        }
                    }
                    else
                    {
                        stateNow = statesIa.chase;
                    }
                }
            }

            yield return new WaitForSeconds(timeAction);
        }
    }

    public void OnTarget(attributes atb)
    {
        if (!atb.CompareTag(tag))
        {
            stateNow = statesIa.chase;
            atbTarget = atb;
        }
    }

    private void FixedUpdate()
    {
        Vector2 moveD = Vector2.zero;
        if (moveTarget)
        {
            //Debug.Log(targetPosition+" tp");
            moveD = targetPosition;
            moveD.x -= transform.position.x;
            moveD.y -= transform.position.y;
            //Debug.Log(moveD+" md");

            if (moveD.magnitude > distTarget)
            {
                moveD.Normalize();

                if (Physics2D.CircleCast(coll.bounds.center, coll.bounds.extents.y, moveD, 1f, repository.GetLayerMaskObst()).collider)
                {
                    moveD = Vector2.zero;
                    moveTarget = false;
                }
            }
            else
            {
                moveD = Vector2.zero;
                moveTarget = false;
            }
        }


        //Debug.Log(moveD);
        atb.GetMovement().Move(moveD);
    }
}
