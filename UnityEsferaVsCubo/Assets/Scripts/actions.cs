using UnityEngine;

public class actions : MonoBehaviour
{
    private Animator anima;
    [SerializeField]
    private weaponInfs weaponNow = null;
    private playerControll playerControll;
    [SerializeField]
    private Transform hand1 = null, hand2 = null;
    // Start is called before the first frame update
    void Start()
    {
        anima = GetComponent<Animator>();
        playerControll = GetComponent<playerControll>();

        GameObject objWp = Instantiate(weaponNow.GetWeaponObj());
        Vector3 posAjust = weaponNow.GetPosHand();
        objWp.transform.SetParent(hand1);
        objWp.transform.localPosition = posAjust;
        if (weaponNow.IsDual())
        {
            objWp = Instantiate(weaponNow.GetWeaponObj());
            posAjust.x *= -1f;
            objWp.transform.SetParent(hand2);
            objWp.transform.localPosition = posAjust;
        }
    }

   
    public void Fire()
    {
        anima.SetBool("fire", true);
    }

    public weaponInfs GetWeaponInfs()
    {
        return weaponNow;
    }

    public void SetWeapon(weaponInfs wp)
    {
        if (weaponNow == wp)
        {
            if (playerControll && wp)
            {
                playerControll.AddBullets(wp.GetLimitBulets() / 5);
            }
        }
        else
        {
            weaponNow = wp;
            if (wp)
            {
                if (playerControll)
                {
                    playerControll.Setullets(wp.GetLimitBulets() / 5);
                }
                GameObject objWp = Instantiate(wp.GetWeaponObj());
                Vector3 posAjust = wp.GetPosHand();
                objWp.transform.SetParent(hand1);
                objWp.transform.localPosition = posAjust;
                if (wp.IsDual())
                {
                    objWp = Instantiate(wp.GetWeaponObj());
                    posAjust.x *= -1f;
                    objWp.transform.SetParent(hand2);
                    objWp.transform.localPosition = posAjust;
                }
            }
        }
    }

    public void StopFire()
    {
        anima.SetBool("fire", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (weaponNow == null) {
            if (collision.CompareTag("Enemie"))
            {
                attributes atb = collision.GetComponentInParent<attributes>();
                Vector2 force = Quaternion.Euler(Vector3.forward*transform.eulerAngles.z) * Vector2.up * 5f;
                atb.AddHp(-5, force);
            }
        }
    }

    public float ReachNow()
    {
        return 1.25f;
    }
}
