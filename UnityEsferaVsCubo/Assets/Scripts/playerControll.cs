using UnityEngine;
using UnityEngine.UI;


public class playerControll : MonoBehaviour
{
    [SerializeField]
    private Transform transformTarget = null;
    [SerializeField]
    private Text textScores = null;
    private int scores = 0;
    private int bullets = 0;
    private movement movementPlayer;
    private attributes atb;
    // Start is called before the first frame update
    void Awake()
    {
        movementPlayer = GetComponent<movement>();
        atb = GetComponent<attributes>();
        AddScores(0);
    }


    public void AddScores(int add)
    {
        scores += add;
        textScores.text = "Pontos: " + scores;
    }

    public void AddBullets(int add)
    {
        actions act = atb.GetActions();
        int limit = act.GetWeaponInfs().GetLimitBulets();
        bullets += add;

        if (bullets < 0)
        {
            act.SetWeapon(null);
        }
        else if (bullets > limit)
        {
            bullets = limit;
        }
    }

    public void Setullets(int value)
    {
        bullets = value;
    }

    // Update is called once per frame
    void Update()
    {
        actions act = atb.GetActions();

        //Movimentação
        Vector2 dire = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movementPlayer.Move(dire);

        //Pegando posição do mouse no mundo
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Comparando com a do jogador
        Vector2 delta = mousePos;
        delta.x -= transform.position.x;
        delta.y -= transform.position.y;
        //Ajustando mira
        float distTarget = delta.magnitude;
        //Caso o mouse estaja mais distance que alcance da arma
        if (distTarget > act.ReachNow())
        {
            distTarget = act.ReachNow();
        }
        Vector2 posTarget = Vector2.up * distTarget;
        transformTarget.localPosition = posTarget;
        //Rotacionar
        float angZ = Vector2.SignedAngle(Vector2.up, delta);
        movementPlayer.TurnTo(angZ);

        //Comando de atirar
        if (Input.GetButton("Fire1"))
        {

            if (Input.GetButtonDown("Fire1"))
            {
                act.Fire();
            }
        }
        
    }

    private void LateUpdate()
    {
        //Fazer camera seguir
        Vector3 posCam = Camera.main.transform.position;
        posCam.x = transform.position.x;
        posCam.y = transform.position.y;
        Camera.main.transform.position = posCam;
    }
}
