using UnityEngine;

[CreateAssetMenu]
public class weaponInfs : ScriptableObject
{
    [SerializeField]
    private int idFire = 0, bulletsLimit = 20;
    [SerializeField]
    private bool dual = false, continuous = false;
    [SerializeField]
    private progetile progetile = null;
    [SerializeField]
    private int damage = 10;
    [SerializeField]
    private Vector3 posHand = Vector3.zero;
    [SerializeField]
    [Range(2f,12f)]
    private float reach = 7f;
    [SerializeField]
    private Vector3 posExitBullet = Vector3.zero;

    public progetile GetBullet()
    {
        return progetile;
    }

    public Vector3 GetPosHand()
    {
        return posHand;
    }

    public bool IsDual()
    {
        return dual;
    }

    public bool IsContinuous()
    {
        return continuous;
    }

    public int GetIdFire()
    {
        return idFire;
    }

    public int GetLimitBulets()
    {
        return bulletsLimit;
    }

    public float GetReach()
    {
        return reach;
    }

    public Vector3 GetPosExitBullet()
    {
        return posExitBullet;
    }

    public int GetDamage()
    {
        return damage;
    }
}
