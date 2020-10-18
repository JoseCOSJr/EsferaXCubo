using UnityEngine;

public class weapon : MonoBehaviour
{
    [SerializeField]
    private weaponInfs weaponInf = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            actions act = collision.GetComponent<actions>();
        }
    }
}
