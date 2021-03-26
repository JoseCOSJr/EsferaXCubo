using UnityEngine;
using UnityEngine.UI;

public class hud : MonoBehaviour
{
    public static hud hudX = null;
    public itemHud[] itemHuds = new itemHud[16];
    public Text textNameItemChosen, textDescription;
    private Image imageItemChosen;
    private item[] itemsInventory = new item[16];
    private int itensCount = 0;
    public GameObject screemInventoryObj;

    // Start is called before the first frame update
    void Start()
    {
        hudX = this;
        imageItemChosen = textNameItemChosen.GetComponentInChildren<Image>();

        for(int i = 0;i < itemsInventory.Length; i++)
        {
            itemsInventory[i] = null;
        }
    }

    public void AddItemInventory(item add)
    {
        if (itensCount < itemsInventory.Length)
        {
            itemsInventory[itensCount] = add;
            itemHuds[itensCount].SetItem(add);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            screemInventoryObj.SetActive(!screemInventoryObj.activeInHierarchy);
            if (screemInventoryObj.activeInHierarchy)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }
}
