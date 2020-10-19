using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class repository : MonoBehaviour
{
    public enum typesObjs { progetiles, weapon, box, enemies}
    private static repository repositoryNow;
    [SerializeField]
    private boxItens baseBoxs = null;
    [SerializeField]
    private List<weapon> allWeapons = new List<weapon>();
    [SerializeField]
    private attributes enemieBase = null;
    private List<GameObject> progetilesList, weaponsList, boxsList, enemiesList;

    public static void InstantiateObjs(GameObject obj ,typesObjs types, int number=1)
    {
        List<GameObject> list = repositoryNow.boxsList;

        if(types == typesObjs.enemies)
        {
            list = repositoryNow.enemiesList;
        }
        else if(types == typesObjs.progetiles)
        {
            list = repositoryNow.progetilesList;
        }
        else if(types == typesObjs.weapon)
        {
            list = repositoryNow.weaponsList;
        }

        for(int i = 0; i < number; i++)
        {
            GameObject objI = Instantiate(obj);
            objI.transform.SetParent(repositoryNow.transform);
            objI.SetActive(false);
            list.Add(objI);
        }
    }

    public static Transform GetTransformRepository()
    {
        return repositoryNow.transform;
    } 

    public static weapon GetWeapon(weapon which)
    {
        List<GameObject> list = repositoryNow.weaponsList.FindAll(x => x.GetComponent<weapon>().NameId() == which.NameId());
        GameObject obj = GetObjInList(list, typesObjs.weapon, which.gameObject);

        return obj.GetComponent<weapon>();
    }

    public static progetile GetBullets(progetile which)
    {
        List<GameObject> list = repositoryNow.progetilesList.FindAll(x => x.GetComponent<progetile>().NameId() == which.NameId());
        GameObject obj = GetObjInList(list, typesObjs.progetiles, which.gameObject);

        return obj.GetComponent<progetile>();
    }

    public static attributes GetEnemie(attributes who)
    {
        List<GameObject> list = repositoryNow.progetilesList.FindAll(x => x.GetComponent<attributes>().NameId() == who.NameId());
        GameObject obj = GetObjInList(list, typesObjs.enemies, who.gameObject);

        return obj.GetComponent<attributes>();
    }

    public static boxItens GetBox()
    {
        GameObject obj = GetObjInList(repositoryNow.boxsList, typesObjs.box, repositoryNow.baseBoxs.gameObject);

        return obj.GetComponent<boxItens>();
    }

    private static GameObject GetObjInList(List<GameObject> list, typesObjs type, GameObject objBase)
    {
        GameObject obj = list.Find(x => !x.activeInHierarchy);
        if (!obj)
        {
            InstantiateObjs(objBase, type);
            obj = list.Find(x => !x.activeInHierarchy);
        }

        return obj;
    }

    private void Awake()
    {
        repositoryNow = this;
        progetilesList = new List<GameObject>();
        weaponsList = new List<GameObject>();
        boxsList = new List<GameObject>();
        enemiesList = new List<GameObject>();

        InstantiateObjs(baseBoxs.gameObject, typesObjs.box, 25);

        InstantiateObjs(enemieBase.gameObject, typesObjs.enemies, 20);

        for(int i = 0; i < allWeapons.Count; i++)
        {
            GameObject obj = allWeapons[i].gameObject;
            int n = 5;
            if (allWeapons[i].GetWeaponInfs().IsDual())
                n = 10;

            InstantiateObjs(obj, typesObjs.weapon, n);
        }
    }
}
