using UnityEngine;
using UnityEngine.UI;

public class attributes : MonoBehaviour
{
    [SerializeField]
    private string nameId = "";
    [SerializeField]
    private int hpMax = 100;
    [SerializeField]
    private Slider sliderHp = null;
    private float hpNow;
    private actions actions;
    private float timeHitStun = 0f;
    private movement movement;
    private ia ia;
    // Start is called before the first frame update
    void Awake()
    {
        hpNow = hpMax;
        ia = GetComponent<ia>();
        movement = GetComponent<movement>();
        actions = GetComponent<actions>();
    }

    public string NameId()
    {
        return nameId;
    }

    public actions GetActions()
    {
        return actions;
    }

    public movement GetMovement()
    {
        return movement;
    }

    public bool InHitStun()
    {
        return timeHitStun > 0f;
    }

    private void FixedUpdate()
    {
        if (timeHitStun > 0f)
            timeHitStun -= Time.fixedDeltaTime;
    }

    public void Respaw(Vector2 pos)
    {
        transform.position = pos;
        hpNow = hpMax;
        timeHitStun = 0f;
        gameObject.SetActive(true);
    }

    public void AddHp(int value, Vector2 force, playerControll playerKiller)
    {
        hpNow += value;
        if (hpNow <= 0f)
        {
            if (playerKiller)
                playerKiller.AddScores(20);

            gameObject.SetActive(false);
        }

        if (force.sqrMagnitude > 0f)
        {
            timeHitStun = 0.5f;
            movement.AddForce(force);
        }

        //Caso inimigo leve tiro do jogador ele vai atras dele
        if (playerKiller && ia)
            ia.OnTarget(playerKiller.GetAttributes());

        sliderHp.value = hpNow / hpMax;
    }

    public bool IsDead()
    {
        return hpNow <= 0;
    }
}
