using UnityEngine;

public class investigationObject : MonoBehaviour
{
    public item itemHere;

    private void OnMouseDown()
    {
        if (itemHere)
        {
            gameObject.SetActive(false);
            hud.hudX.AddItemInventory(itemHere);
        }
    }

}
