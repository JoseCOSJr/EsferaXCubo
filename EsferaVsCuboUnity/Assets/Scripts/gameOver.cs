using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class gameOver : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Text textScores;
    public GameObject gameOverObj;
    private Image image;
    private float timeCount = 0f;
    private bool load = false;

    // Start is called before the first frame update
    void Start()
    {
        textScores.gameObject.SetActive(false);
        gameOverObj.SetActive(false);
        image = GetComponent<Image>();
        image.color = Color.clear;

        audioMixer.SetFloat("VolumeInGameEfx", -80f);
        audioMixer.SetFloat("VolumeInGame", -80f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!gameOverObj.activeInHierarchy)
        {
            timeCount += Time.deltaTime;
            if (timeCount < 1f)
            {
                image.color = Color.Lerp(Color.clear, Color.black, timeCount);
            }
            else if (timeCount < 2f)
            {
                if (image.color != Color.black)
                    image.color = Color.black;
            }
            else if (timeCount < 3f)
            {
                textScores.gameObject.SetActive(true);
            }
            else if (timeCount < 4f)
            {
                gameOverObj.SetActive(true);
            }
        }
        else if(Input.anyKeyDown && !load)
        {
            load = true;
            SceneManager.LoadSceneAsync(0);
        }
    }
}
