using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipsPlayer: MonoBehaviour
{
    [Tooltip("索引0: 怪物剛見到玩家, 1: 怪物攻擊, 2: 怪物受傷, 3: 怪物死亡")]
    public List<AudioSource> audioSources;
    private bool alreadyPlayIntro = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            audioSources[0].Play();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            audioSources[1].Play();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            audioSources[2].Play();
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            audioSources[3].Play();
        }
    }

    public void PlayAudioClip(int num)
    {
        if(num == 0)
        {
            if (!alreadyPlayIntro)
            {
                audioSources[num].Play();
                alreadyPlayIntro = true;
            }
            else if(alreadyPlayIntro)
            {
                Debug.Log("AlreadyPlayIntro");
                return;
            }
        }
        else
        {
            audioSources[num].Play();
        }
    }
}
