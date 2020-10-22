using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class repository : MonoBehaviour
{
    public enum typesObjs { progetiles, weapon, box, enemies, audioSoucer}
    private static repository repositoryNow;
    [SerializeField]
    private boxItens baseBoxs = null;
    [SerializeField]
    private List<weapon> allWeapons = new List<weapon>();
    [SerializeField]
    private attributes enemieBase = null;
    [SerializeField]
    private AudioSource audioSourceBase = null;
    [SerializeField]
    private LayerMask layerMaskObst, layerMaskChars;
    public AudioClip clipPunch, clipPuchHit, clipBoxCatch;
    private List<GameObject> progetilesList, weaponsList, boxsList, enemiesList, audioSourceList;

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
        else if(types == typesObjs.audioSoucer)
        {
            list = repositoryNow.audioSourceList;
        }

        for (int i = 0; i < number; i++)
        {
            GameObject objI = Instantiate(obj);
            objI.transform.SetParent(repositoryNow.transform);
            if (types != typesObjs.audioSoucer)
                objI.SetActive(false);

            list.Add(objI);
        }
    }

    public static repository GetRepository()
    {
        return repositoryNow;
    } 

    public static LayerMask GetLayerMaskObst()
    {
        return repositoryNow.layerMaskObst;
    }

    public static LayerMask GetLayerMaskChars()
    {
        return repositoryNow.layerMaskChars;
    }

    public static weapon GetWeapon(weapon which)
    {
        GameObject obj = null;

        while (!obj)
        {
            List<GameObject> list = repositoryNow.weaponsList.FindAll(x => x.GetComponent<weapon>().NameId() == which.NameId());
            obj = GetObjInList(list, typesObjs.weapon, which.gameObject);
        }

        weapon wp = obj.GetComponent<weapon>();
        wp.UseWeapon();
        return wp;
    }


    public static weapon GetRandomWeapon()
    {
        int id = Random.Range(0, repositoryNow.allWeapons.Count);
        weapon wp = repositoryNow.allWeapons[id];

        return GetWeapon(wp);
    }

    public static progetile GetBullets(progetile which)
    {
        GameObject obj = null;

        while (!obj)
        {
            List<GameObject> list = repositoryNow.progetilesList.FindAll(x => x.GetComponent<progetile>().NameId() == which.NameId());
            obj = GetObjInList(list, typesObjs.progetiles, which.gameObject);
        }

        return obj.GetComponent<progetile>();
    }

    public static attributes GetEnemie()
    {
        GameObject obj = null;

        while (!obj)
        {
            //Caso adicione outros inimigos o codigo já vai aceitar
            List<GameObject> list = repositoryNow.enemiesList.FindAll(x => x.GetComponent<attributes>().NameId() == repositoryNow.enemieBase.NameId());
            obj = GetObjInList(list, typesObjs.enemies, repositoryNow.enemieBase.gameObject);
        }

        return obj.GetComponent<attributes>();
    }

    public static int NumberEnemiesLive()
    {
        return repositoryNow.enemiesList.FindAll(x => x.activeInHierarchy).Count;
    }

    public static boxItens GetBox()
    {
        GameObject obj = null;

        while (!obj)
        {
            obj = GetObjInList(repositoryNow.boxsList, typesObjs.box, repositoryNow.baseBoxs.gameObject);
        }

        return obj.GetComponent<boxItens>();
    }

    public static int NumbersBoxEnable()
    {
        return repositoryNow.boxsList.FindAll(x => x.activeInHierarchy).Count;
    }

    private static GameObject GetObjInList(List<GameObject> list, typesObjs type, GameObject objBase)
    {
        GameObject obj = list.Find(x => !x.activeInHierarchy);
        if (type == typesObjs.audioSoucer)
            obj = list.Find(x => !x.GetComponent<AudioSource>().isPlaying);

        if (!obj)
        {
            InstantiateObjs(objBase, type, 5);
        }
        else
        {
            obj.SetActive(true);
        }

        return obj;
    }

    public static AudioSource GetAudioSource()
    {
        GameObject obj = null;

        while (!obj)
        {
            obj = GetObjInList(repositoryNow.audioSourceList, typesObjs.audioSoucer, repositoryNow.audioSourceBase.gameObject);
        }

        return obj.GetComponent<AudioSource>();
    }

    private void Awake()
    {
        repositoryNow = this;
        progetilesList = new List<GameObject>();
        weaponsList = new List<GameObject>();
        boxsList = new List<GameObject>();
        enemiesList = new List<GameObject>();
        audioSourceList = new List<GameObject>();

        InstantiateObjs(baseBoxs.gameObject, typesObjs.box, 50);
        InstantiateObjs(audioSourceBase.gameObject, typesObjs.audioSoucer, 50);
        InstantiateObjs(enemieBase.gameObject, typesObjs.enemies, 50);


        for(int i = 0; i < allWeapons.Count; i++)
        {
            GameObject obj = allWeapons[i].gameObject;
            int n = 20;
            if (allWeapons[i].GetWeaponInfs().IsDual())
                n *= 2;

            InstantiateObjs(obj, typesObjs.weapon, n);
        }
    }
}
