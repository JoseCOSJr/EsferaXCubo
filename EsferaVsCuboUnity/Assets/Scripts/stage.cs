using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;

public class stage : MonoBehaviour
{
    private static float constantDificultTime = 60f;
    public List<AudioClip> clipsStage = new List<AudioClip>();
    [SerializeField]
    private List<Renderer> areasStage = new List<Renderer>();
    [SerializeField]
    private AudioSource audioSourceStage = null;
    private float timeCount = 0f;
    public AudioMixer audioMixer;

    private void Start()
    {
        audioMixer.SetFloat("VolumeInGameEfx", 0f);
        audioMixer.SetFloat("VolumeInGame", 0f);
    }

    void Update()
    {
        if (!audioSourceStage.isPlaying)
            SetMusic();

        if(EnemiesNeed() >= repository.NumberEnemiesLive())
            RespawEnemie();

        if (BoxNeed() >= repository.NumbersBoxEnable())
            RespawBox();

        timeCount += Time.deltaTime;
    }

    private void SetMusic()
    {
        List<AudioClip> list = clipsStage.FindAll(x => x != audioSourceStage.clip);
        int id = Random.Range(0, list.Count);
        AudioClip clip = list[id];
        audioSourceStage.PlayOneShot(clip);
    }

    private void RespawEnemie()
    {
        attributes atbEnemie = repository.GetEnemie();
        atbEnemie.Respaw(GetRandomPos());
    }

    private void RespawBox()
    {
        boxItens box = repository.GetBox();
        box.Respaw(GetRandomPos());
    }

    private Vector2 GetRandomPos()
    {
        List<Renderer> renderersList = areasStage.FindAll(x => !x.isVisible);
        int id = Random.Range(0, renderersList.Count);
        Bounds bd = renderersList[id].bounds;
        Vector2 pos = bd.center;
        float xValue = bd.extents.x * 0.9f;
        float yValue = bd.extents.y * 0.9f;
        pos.x += Random.Range(-xValue, xValue);
        pos.y += Random.Range(-yValue, yValue);

        return pos;
    }

    private int EnemiesNeed()
    {
        float delta = 9f + timeCount*18f / (timeCount + constantDificultTime);

        return (int)delta;
    }

    private int BoxNeed()
    {
        float delta = 18f - timeCount * 9f / (timeCount + constantDificultTime);

        return (int)delta;
    }
}
