using UnityEngine;

public class weapon : MonoBehaviour
{
    [SerializeField]
    private weaponInfs weaponInf = null;
    [SerializeField]
    private int numberBulletsInstantie = 10;
    [SerializeField]
    private Vector3 sclOriginal;

    private void Awake()
    {
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

    //Ia colocar para pegar a arma
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            actions act = collision.GetComponent<actions>();
            act.SetWeapon(this);
        }
    }*/
}
