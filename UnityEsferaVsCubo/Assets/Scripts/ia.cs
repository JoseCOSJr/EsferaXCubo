using UnityEngine;
using System.Collections;

public class ia : MonoBehaviour
{
    private float timeAction = 0.1f;
    private attributes atb;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<attributes>();
    }

    private void OnEnable()
    {
        StartCoroutine(IAAtive());
    }

    IEnumerator IAAtive()
    {
        while (true)
        {
            if(atb && atb.IsDead())
            {

            }

            yield return new WaitForSeconds(timeAction);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
