using UnityEditor;
using UnityEngine;

public class actions : MonoBehaviour
{
    private Animator anima;
    private weapon weaponNow1 = null, weaponNow2 = null;
    private playerControll playerControll;
    [SerializeField]
    private Transform hand1 = null, hand2 = null;
    private attributes atb;
    // Start is called before the first frame update
    void Start()
    {
        anima = GetComponent<Animator>();
        playerControll = GetComponent<playerControll>();
        atb = GetComponent<attributes>();

        weapon wp = null;
        if (CompareTag("Enemie"))
        {
            wp = repository.GetRandomWeapon();
        }

        SetWeapon(wp);
    }


    private void OnEnable()
    {
        if (weaponNow1) 
        {
            SetWeapon(null);
            weapon wp = repository.GetRandomWeapon();
            SetWeapon(wp);
        }
    }

    public void Fire()
    {
        if (!anima.GetBool("fire"))
        {
            anima.SetBool("fire", true);
            if (!weaponNow1)
            {
                AudioClip clipHit = repository.GetRepository().clipPunch;
                repository.GetAudioSource().PlayOneShot(clipHit);
            }
        }
    }

    public weaponInfs GetWeaponInfs()
    {
        if (weaponNow1)
            return weaponNow1.GetWeaponInfs();

        return null;
    }

    public void SetWeapon(weapon wp)
    {
        if (weaponNow1 == wp)
        {
            if (playerControll && wp)
            {
                playerControll.AddAmmunition(wp.GetWeaponInfs().GetLimitBulets() / 5);
            }
        }
        else
        {
            if (wp)
            {
                DisableWeaponNow();
                wp.gameObject.SetActive(true);
                GameObject objWp = wp.gameObject;
                objWp.SetActive(objWp);
                Vector3 posAjust = wp.GetWeaponInfs().GetPosHand();
                objWp.transform.SetParent(hand1,true);
                objWp.transform.localPosition = posAjust;
                objWp.transform.localEulerAngles = Vector3.zero;
                weaponNow1 = wp;
                wp.OriginalScale();
                if (wp.GetWeaponInfs().IsDual())
                {
                    weaponNow2 = repository.GetWeapon(wp);
                    weaponNow2.gameObject.SetActive(true);
                    objWp = weaponNow2.gameObject;
                    objWp.SetActive(true);
                    posAjust.x *= -1f;
                    objWp.transform.SetParent(hand2, true);
                    objWp.transform.localPosition = posAjust;
                    objWp.transform.localEulerAngles = Vector3.zero;
                    weaponNow2.OriginalScale();
                }

                anima.SetFloat("weapon", wp.GetWeaponInfs().GetIdFire());
                if (playerControll)
                {
                    playerControll.SetAmmunition(wp.GetWeaponInfs().GetLimitBulets() / 5);
                }
            }
            else if(weaponNow1)
            {
                anima.SetFloat("weapon", 0);
                DisableWeaponNow();
                weaponNow1 = wp;
            }
        }
    }


    private void DisableWeaponNow()
    {
        if (weaponNow1)
        {
            weaponNow1.gameObject.SetActive(false);
            if (weaponNow2)
            {
                weaponNow2.gameObject.SetActive(false);
                weaponNow2 = null;
            }
        }
    }

    public void StopFire()
    {
        anima.SetBool("fire", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (weaponNow1 == null) 
        {
            if (collision.CompareTag("Enemie"))
            {
                AudioClip clipHit = repository.GetRepository().clipPuchHit;
                repository.GetAudioSource().PlayOneShot(clipHit);
                attributes atb = collision.GetComponentInParent<attributes>();
                Vector2 force = Quaternion.Euler(Vector3.forward*transform.eulerAngles.z) * Vector2.up * 5f;
                atb.AddHp(-10, force, playerControll);
            }
        }
    }

    public float ReachNow()
    {
        if (weaponNow1)
            return GetWeaponInfs().GetReach() + GetWeaponInfs().GetPosHand().y + GetWeaponInfs().GetPosExitBullet().y;

        return 1.25f;
    }

    public playerControll GetPlayerControll()
    {
        return playerControll;
    }

    public void InocationBullet()
    {
        if (weaponNow1)
        {          
            weaponInfs wpInf = GetWeaponInfs();
            progetile p = wpInf.GetBullet();
            p = repository.GetBullets(p);
            //Posição da saida da bala
            Vector3 pos = hand1.position + transform.rotation * wpInf.GetPosExitBullet();
            //Posição da mira do personagem
            Vector3 targetPos = transform.up * (wpInf.GetReach()+hand1.localPosition.y+wpInf.GetPosExitBullet().y) + transform.position;
            //Calculando direção do tiro
            Vector3 dire = targetPos - pos;
            dire.z = 0f;
            p.Inocation(wpInf, pos, dire.normalized, atb);
            //Consumo de balas
            int n = 1;
            //É uma arma dupla?
            if (wpInf.IsDual())
            {
                n = 2;
                p = wpInf.GetBullet();
                p = repository.GetBullets(p);
                //Posição da saida da bala
                pos = hand2.position + transform.rotation * wpInf.GetPosExitBullet();
                //Posição da mira do personagem
                targetPos = transform.up * (wpInf.GetReach() + hand2.localPosition.y+ wpInf.GetPosExitBullet().y) + transform.position;
                //Calculando direção do tiro
                dire = targetPos - pos;
                dire.z = 0f;
                p.Inocation(wpInf, pos, dire.normalized, atb);
            }

            if (playerControll)
            {
                playerControll.AddAmmunition(-n);
            }
        }
    }
}
