using UnityEditor;
using UnityEngine;

public class actions : MonoBehaviour
{
    private Animator anima;
    [SerializeField]
    private weapon weaponBegin = null;
    private weapon weaponNow = null;
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

        if (weaponBegin)
        {
            weaponBegin = repository.GetWeapon(weaponBegin);
        }

        SetWeapon(weaponBegin);
    }

   
    public void Fire()
    {
        anima.SetBool("fire", true);
        if (!weaponNow)
        {
            AudioClip clipHit = (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/Souds/Efx/9509__petenice__whoosh.wav", typeof(AudioClip));
            repository.GetAudioSource().PlayOneShot(clipHit);
        }
    }

    public weaponInfs GetWeaponInfs()
    {
        if (weaponNow)
            return weaponNow.GetWeaponInfs();

        return null;
    }

    public void SetWeapon(weapon wp)
    {
        if (weaponNow == wp)
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
                GameObject objWp = wp.gameObject;
                objWp.SetActive(objWp);
                Vector3 posAjust = wp.GetWeaponInfs().GetPosHand();
                objWp.transform.SetParent(hand1);
                objWp.transform.localPosition = posAjust;
                objWp.transform.localEulerAngles = Vector3.zero;
                if (wp.GetWeaponInfs().IsDual())
                {
                    weapon wp2 = repository.GetWeapon(wp);
                    objWp = wp2.gameObject;
                    objWp.SetActive(true);
                    posAjust.x *= -1f;
                    objWp.transform.SetParent(hand2);
                    objWp.transform.localPosition = posAjust;
                    objWp.transform.localEulerAngles = Vector3.zero;
                }

                weaponNow = wp;
                anima.SetFloat("weapon", wp.GetWeaponInfs().GetIdFire());
                if (playerControll)
                {
                    playerControll.SetAmmunition(wp.GetWeaponInfs().GetLimitBulets() / 5);
                }
            }
            else if(weaponNow)
            {
                anima.SetFloat("weapon", 0);
                DisableWeaponNow();
                weaponNow = wp;
            }
        }
    }


    private void DisableWeaponNow()
    {
        if (weaponNow)
        {
            hand1.GetChild(0).gameObject.SetActive(false);
            if (weaponNow.GetWeaponInfs().IsDual())
            {
                hand2.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    public void StopFire()
    {
        anima.SetBool("fire", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (weaponNow == null) 
        {
            if (collision.CompareTag("Enemie"))
            {
                AudioClip clipHit = (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/Souds/Efx/448982__ethanchase7744__punch.wav", typeof(AudioClip));
                repository.GetAudioSource().PlayOneShot(clipHit);
                attributes atb = collision.GetComponentInParent<attributes>();
                Vector2 force = Quaternion.Euler(Vector3.forward*transform.eulerAngles.z) * Vector2.up * 5f;
                atb.AddHp(-10, force, playerControll);
            }
        }
    }

    public float ReachNow()
    {
        if (weaponNow)
            return GetWeaponInfs().GetReach() + GetWeaponInfs().GetPosHand().y + GetWeaponInfs().GetPosExitBullet().y;

        return 1.25f;
    }

    public playerControll GetPlayerControll()
    {
        return playerControll;
    }

    public void InocationBullet()
    {
        if (weaponNow)
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
