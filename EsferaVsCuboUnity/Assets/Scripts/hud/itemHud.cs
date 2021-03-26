using UnityEngine;
using UnityEngine.UI;

public class itemHud : MonoBehaviour
{
    private static float sizeImage = 50f;
    public Image imageItem;
    public Text textNameItem;
    private item itemHere = null;

    private void OnEnable()
    {
        if (itemHere)
        {
            imageItem.enabled = true;
            textNameItem.text = itemHere.nameItem;
            imageItem.sprite = itemHere.spriteItem;
            Vector2 size = itemHere.spriteItem.rect.size;
            float auxSize = size.x;
            if (auxSize < size.y)
            {
                auxSize = size.y;
            }

            float sizeMult = sizeImage / auxSize;
            imageItem.rectTransform.sizeDelta = size * sizeMult;

            textNameItem.text = itemHere.nameItem;
        }
        else
        {
            imageItem.enabled = false;
            textNameItem.text = "";
        }
    }


    public void SetItem(item item)
    {
        itemHere = item;
    }
}
