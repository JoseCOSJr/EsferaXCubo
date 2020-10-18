using UnityEngine;
using UnityEngine.UI;

public class attributes : MonoBehaviour
{
    [SerializeField]
    private int hpMax = 100;
    [SerializeField]
    private Slider sliderHp = null;
    private float hpNow;
    private actions actions;
    private float timeHitStun = 0f;
    private movement movement;
    // Start is called before the first frame update
    void Start()
    {
        hpNow = hpMax;
        movement = GetComponent<movement>();
        actions = GetComponent<actions>();
    }

    public actions GetActions()
    {
        return actions;
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

    public void AddHp(int value, Vector2 force)
    {
        hpNow += value;
        if (hpNow <= 0f)
            gameObject.SetActive(false);

        if (force.sqrMagnitude > 0f)
        {
            timeHitStun = 0.5f;
            movement.AddForce(force);
        }

        sliderHp.value = hpNow / hpMax;
    }
}
