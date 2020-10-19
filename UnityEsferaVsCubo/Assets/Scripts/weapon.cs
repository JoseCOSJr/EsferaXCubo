using UnityEngine;

public class weapon : MonoBehaviour
{
    [SerializeField]
    private weaponInfs weaponInf = null;
    [SerializeField]
    private GameObject objWeapon = null;
    private GameObject objWeapon2 = null;
    [SerializeField]
    private int numberBulletsInstantie = 10;


    private void Awake()
    {
        repository.InstantiateObjs(weaponInf.GetBullet().gameObject, repository.typesObjs.progetiles, numberBulletsInstantie);
        objWeapon = Instantiate(objWeapon);
        objWeapon.transform.SetParent(repository.GetTransformRepository());
        objWeapon.SetActive(false);
        if (weaponInf.IsDual())
        {
            objWeapon2 = Instantiate(objWeapon);
            objWeapon2.transform.SetParent(repository.GetTransformRepository());
            objWeapon2.SetActive(false);
        }
    }

    public string NameId()
    {
        return weaponInf.name;
    }

    public weaponInfs GetWeaponInfs()
    {
        return weaponInf;
    }

    public GameObject GetWeponObj()
    {
        if (objWeapon.activeInHierarchy)
            return objWeapon2;

        return objWeapon;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            actions act = collision.GetComponent<actions>();
            act.SetWeapon(this, false);
        }
    }
}
