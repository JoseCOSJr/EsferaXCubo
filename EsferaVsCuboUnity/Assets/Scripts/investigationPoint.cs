using UnityEngine;

public class investigationPoint : MonoBehaviour
{
    public GameObject objAtiveInvestigation;
    //[SerializeField]
    private bool onInvestigation = false, mouseAbove = false;
    private float countTime = 0f;

    private void Update()
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, 2f, repository.GetLayerMaskChars());
        bool playerInReach = false;
       
        for(int i = 0; i < colls.Length && !playerInReach; i++)
        {
            if (colls[i].CompareTag("Player"))
            {
                playerInReach = true;
            }
        }

        if (playerInReach && countTime <= 0f)
        {
            if (onInvestigation)
            {
                if (Input.GetButtonDown("Fire2"))
                {
                    onInvestigation = false;
                    countTime = 1f;
                }
            }
            else if (Input.GetButtonDown("Fire1") && mouseAbove)
            {
                onInvestigation = true;
                countTime = 1f;
                playerControll.canMove = false;
                objAtiveInvestigation.SetActive(true);
            }
        }
    }

    private void LateUpdate()
    {
        if (countTime > 0f)
        {
            countTime -= Time.deltaTime*2f;
            float t = countTime;
            if (onInvestigation)
            {
                t = 1f - countTime;
            }
            else if(t <= 0f)
            {
                objAtiveInvestigation.SetActive(false);
                playerControll.canMove = true;
            }

            Vector3 posCam = Camera.main.transform.position;
            posCam.z = transform.position.z;
            Vector3 newPos = Vector3.Lerp(transform.position, posCam, t);
            Vector3 newScl = Vector3.Lerp(Vector3.zero, Vector3.one, t);
            objAtiveInvestigation.transform.position = newPos;
            objAtiveInvestigation.transform.localScale = newScl;
        }
    }

    private void OnMouseEnter()
    {
        mouseAbove = true;
    }


    private void OnMouseExit()
    {
        mouseAbove = false;
    }
}
