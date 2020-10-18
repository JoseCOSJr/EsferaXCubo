using UnityEngine;

[CreateAssetMenu]
public class weaponInfs : ScriptableObject
{
    [SerializeField]
    private int idFire = 0, bulletsLimit = 20;
    [SerializeField]
    private GameObject weaponObj = null;
    [SerializeField]
    private bool dual = false;
    [SerializeField]
    private progetile progetile = null;
    [SerializeField]
    private int damage = 10;
    [SerializeField]
    private Vector3 posHand = Vector3.zero;


    public GameObject GetWeaponObj()
    {
        return weaponObj;
    }

    public Vector3 GetPosHand()
    {
        return posHand;
    }

    public bool IsDual()
    {
        return dual;
    }

    public int GetIdFire()
    {
        return idFire;
    }

    public int GetLimitBulets()
    {
        return bulletsLimit;
    }

    public void AplicationDamage()
    {

    } 
}
