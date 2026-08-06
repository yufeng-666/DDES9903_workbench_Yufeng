using UnityEngine;
using System.Collections;

public class DogFollowAndTalk : MonoBehaviour
{
    [Header("音频")]
    public AudioSource dogSound;
    public AudioSource grandpaVoice;
    public AudioSource timVoice;

    [Header("玩家")]
    public Transform player;

    [Header("跟随设置")]
    public float keepDistance = 1.8f;
    public float speed = 2f;

    private bool triggeredDialog = false;
    private bool startFollow = false;

    void Update()
    {
        // 开启跟随之后小狗一直跟着玩家
        if (startFollow && player != null)
        {
            float dist = Vector3.Distance(transform.position, player.position);
            if (dist > keepDistance)
            {
                Vector3 dir = (player.position - transform.position).normalized;
                transform.position += dir * speed * Time.deltaTime;
                transform.LookAt(player);
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        // 玩家走进小狗触发区域，只触发一次对话
        if (col.CompareTag("Player") && !triggeredDialog)
        {
            triggeredDialog = true;
            StartCoroutine(PlayAudioOrder());
        }
    }

    IEnumerator PlayAudioOrder()
    {
        // 1、小狗音效
        if (dogSound != null && dogSound.clip != null)
        {
            dogSound.Play();
            yield return new WaitForSeconds(dogSound.clip.length);
        }

        // 2、祖父语音
        if (grandpaVoice != null && grandpaVoice.clip != null)
        {
            grandpaVoice.Play();
            yield return new WaitForSeconds(grandpaVoice.clip.length);
        }

        // 3、Tim内心语音
        if (timVoice != null && timVoice.clip != null)
        {
            timVoice.Play();
            yield return new WaitForSeconds(timVoice.clip.length);
        }

        // 三段语音全部播放完毕，小狗开始跟随玩家
        startFollow = true;
    }
}