using UnityEngine;

public class weapon : MonoBehaviour
{
    [SerializeField]
    private weaponInfs weaponInf = null;
    [SerializeField]
    private int numberBulletsInstantie = 10;
    [SerializeField]
    private Vector3 sclOriginal;
    private Collider2D coll;
    private float timeCount;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        repository.InstantiateObjs(weaponInf.GetBullet().gameObject, repository.typesObjs.progetiles, numberBulletsInstantie);
    }

    public string NameId()
    {
        return weaponInf.name;
    }

    public weaponInfs GetWeaponInfs()
    {
        return weaponInf;
    }

    public void OriginalScale()
    {
        Vector3 sclParent = Vector3.one;

        if (transform.parent)
            sclParent = transform.parent.lossyScale;

        Vector3 newScl = sclOriginal;
        newScl.x /= sclParent.x;
        newScl.y /= sclParent.y;
        newScl.z /= sclParent.z;
        transform.localScale = newScl;
    }

    public void Respaw(Vector3 pos)
    {
        pos.z = 0f;
        transform.position = pos;
        gameObject.SetActive(true);
        coll.enabled = true;
        timeCount = 0f;
    }

    private void OnEnable()
    {
        coll.enabled = false;
        transform.SetParent(repository.GetRepository().transform);
    }


    private void Update()
    {
        if (coll.enabled)
        {
            timeCount += Time.deltaTime;

            if (timeCount > 10f)
            {
                gameObject.SetActive(false);
            }
            else
            {
                Vector3 rotate = Vector3.forward * 360f * Time.deltaTime * 2f;
                transform.Rotate(rotate);
            }
        }
    }

    //Colocar para pegar a arma
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            actions act = collision.GetComponent<actions>();
            UseWeapon();
            act.SetWeapon(this);
        }
        else
        {
            gameObject.SetActive(false);
        }     
    }

    public void UseWeapon()
    {
        transform.eulerAngles = Vector3.zero;
        coll.enabled = false;
    }
}
