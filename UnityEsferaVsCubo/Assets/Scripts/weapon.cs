using UnityEngine;

public class weapon : MonoBehaviour
{
    [SerializeField]
    private weaponInfs weaponInf = null;
    [SerializeField]
    private int numberBulletsInstantie = 10;


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
