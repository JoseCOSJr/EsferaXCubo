using UnityEngine;

public class actions : MonoBehaviour
{
    private Animator anima;
    [SerializeField]
    private weapon weaponNow = null;
    private playerControll playerControll;
    [SerializeField]
    private Transform hand1 = null, hand2 = null;
    // Start is called before the first frame update
    void Start()
    {
        anima = GetComponent<Animator>();
        playerControll = GetComponent<playerControll>();

        if (weaponNow)
        {
            weaponNow = repository.GetWeapon(weaponNow);
        }

        SetWeapon(weaponNow, true);
    }

   
    public void Fire()
    {
        anima.SetBool("fire", true);
    }

    public weaponInfs GetWeaponInfs()
    {
        if (weaponNow)
            return weaponNow.GetWeaponInfs();

        return null;
    }

    public void SetWeapon(weapon wp, bool forced)
    {
        if (weaponNow == wp && !forced)
        {
            if (playerControll && wp)
            {
                playerControll.AddBullets(wp.GetWeaponInfs().GetLimitBulets() / 5);
            }
        }
        else
        {
            weaponNow = wp;
            if (wp)
            {
                anima.SetFloat("weapon", wp.GetWeaponInfs().GetIdFire());
                if (playerControll)
                {
                    playerControll.Setullets(wp.GetWeaponInfs().GetLimitBulets() / 5);
                }

                GameObject objWp = wp.GetWeponObj();
                objWp.SetActive(true);
                Vector3 posAjust = wp.GetWeaponInfs().GetPosHand();
                objWp.transform.SetParent(hand1);
                objWp.transform.localPosition = posAjust;
                if (wp.GetWeaponInfs().IsDual())
                {
                    objWp = wp.GetWeponObj();
                    objWp.SetActive(true);
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
        if (weaponNow)
            return GetWeaponInfs().GetReach() + GetWeaponInfs().GetPosHand().y + GetWeaponInfs().GetPosExitBullet().y;

        return 1.25f;
    }

    public void InocationBullet()
    {
        if (weaponNow)
        {
            weaponInfs wpInf = GetWeaponInfs();
            progetile p = wpInf.GetBullet();
            p = repository.GetBullets(p);

            Vector3 pos = hand1.position + transform.rotation * wpInf.GetPosExitBullet();
            p.Inocation(wpInf, pos, transform.up, tag);

            if (wpInf.IsDual())
            {
                p = wpInf.GetBullet();
                p = repository.GetBullets(p);

                pos = hand2.position + transform.rotation * wpInf.GetPosExitBullet();
                p.Inocation(wpInf, pos, transform.up, tag);
            }
        }
    }
}
