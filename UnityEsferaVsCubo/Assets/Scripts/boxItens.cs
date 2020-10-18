using UnityEngine;

public class boxItens : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerControll pct = collision.GetComponentInParent<playerControll>();

        if (pct)
            BoxDisable(pct);
    }

    private void BoxDisable(playerControll p)
    {
        p.AddScores(10);
        gameObject.SetActive(false);
    }
}
