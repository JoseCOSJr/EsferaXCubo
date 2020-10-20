using UnityEditor;
using UnityEngine;

public class boxItens : MonoBehaviour
{
    public void Respaw(Vector2 pos)
    {
        //Mudar posição mantendo posição Z em ceto valor
        Vector3 posNew = pos;
        posNew.z = 1f;
        transform.position = posNew;

        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerControll pct = collision.GetComponentInParent<playerControll>();
        progetile prt = collision.GetComponent<progetile>();

        if (pct || prt && prt.GetAtbOwner().GetActions().GetPlayerControll())
        {
            if (!pct)
                pct = prt.GetAtbOwner().GetActions().GetPlayerControll();

            AddItem(pct);
        }

        BoxDisable(pct);
    }

    private void BoxDisable(playerControll p)
    {
        if (p)
            p.AddScores(10);

        gameObject.SetActive(false);
    }

    private void AddItem(playerControll pct)
    {
        //Chances de pegar arma ou munição
        float luck = Random.value;

        //Caso dê um certo valor aleatorio ou jogador não tenha nenhuma arma ele recbe nova arma
        if (luck > 0.8f || !pct.GetAttributes().GetActions().GetWeaponInfs())
        {
            //Pegando arma aleatoria
            weapon wp = repository.GetRandomWeapon();
            pct.GetAttributes().GetActions().SetWeapon(wp);
        }
        else
        {
            //Ganhando munição
            pct.AddAmmunition(0.1f);
        }

        AudioClip clipHit = (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/Souds/Efx/260437__roganmcdougald__combat-knife-sheath.wav", typeof(AudioClip));
        repository.GetAudioSource().PlayOneShot(clipHit);
    }
}
