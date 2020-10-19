using UnityEngine;

public class progetileBazuka : progetile
{
    [SerializeField]
    private damageArea damageRespaw = null;
    private bool ative;

    protected override void Awake()
    {
        base.Awake();

        damageRespaw = Instantiate(damageRespaw);
        damageRespaw.transform.SetParent(transform.parent);
        damageRespaw.gameObject.SetActive(false);
        ative = false;
    }

    public override void DamgeAplication(attributes atb, float multiply, Vector2 force)
    {
        ative = true;
        base.DamgeAplication(atb, 0.5f * multiply, force);
    }

    private void OnDisable()
    {
        if (ative)
        {
            damageRespaw.Invocation(this, transform.position + Vector3.forward * -3f);
            ative = false;
        }
    }
}
