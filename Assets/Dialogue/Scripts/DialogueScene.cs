using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueScene : MonoBehaviour
{
    public DialogueManager manager;
    public bool keepPlaying = true; // Once keepPlaying is false, the dialogue will stop playing

    [Header("Dialogue Lines for Scene")]
    public List<DialogueLine> scene;

    private Coroutine playingCoroutine;

    void Update()
    {
        if (!keepPlaying && playingCoroutine != null)
        {
            StopCoroutine(playingCoroutine);
            playingCoroutine = null;
        }
    }

    public void PlayAudio()
    {
        if (playingCoroutine == null)
        {
            playingCoroutine = StartCoroutine(PlayScene());
        }
    }

    public void StopAudiio()
    {
        keepPlaying = false;
    }

    IEnumerator PlayScene()
    {
        if (scene != null)
        {
            for (int i = 0; i < scene.Count; i++)
            {
                if (!keepPlaying) break;

                AudioClip clip = scene[i].line;
                AudioSource source = manager.sources[(int)scene[i].character];

                source.PlayOneShot(clip);

                yield return new WaitForSeconds(clip.length + scene[i].timeAfterSound);
            }
        }
        else
        {
            Debug.Log("There is no dialogue lines in the script");
        }
        

        playingCoroutine = null;
    }
}
