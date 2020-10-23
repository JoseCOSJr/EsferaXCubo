using UnityEngine;

public class progetileSmg : progetile
{
    [SerializeField]
    private float spreading = 30f;

    protected override void Awake()
    {
        base.Awake();
        Vector2 newSpeed = Quaternion.Euler(Vector3.forward * spreading) * body.velocity;
        VelocityChange(newSpeed);
    }
}
