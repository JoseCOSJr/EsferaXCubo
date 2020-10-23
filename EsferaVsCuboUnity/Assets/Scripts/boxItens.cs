using UnityEditor;
using UnityEngine;

public class boxItens : MonoBehaviour
{
    public void Respaw(Vector2 pos)
    {
        //Mudar posição mantendo posição Z em ceto valor
        Vector3 posNew = pos;
        posNew.z = 0f;
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
        }

        BoxDisable(pct);
    }

    private void BoxDisable(playerControll p)
    {
        if (p)
            p.AddScores(10);

        AddItem();
        gameObject.SetActive(false);
    }

    private void AddItem()
    {
        //Pegando arma aleatoria
        weapon wp = repository.GetRandomWeapon();
        wp.Respaw(transform.position);

        AudioClip clipHit = repository.GetRepository().clipBoxCatch;
        repository.GetAudioSource().PlayOneShot(clipHit);
    }
}
