using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GrandpaTalkTrigger : MonoBehaviour
{
    [Header("触发对话语音")]
    public AudioSource timAudio;
    public AudioSource grandpaTalkAudio;

    [Header("两个选择按钮")]
    public GameObject btnA;
    public GameObject btnB;

    [Header("选项各自语音")]
    public AudioSource voiceOption1;
    public AudioSource voiceOption2;

    [Header("围栏物体")]
    public GameObject fence;

    private bool triggered = false;

    void Start()
    {
        if (btnA) btnA.SetActive(false);
        if (btnB) btnB.SetActive(false);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!triggered && col.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(VoiceFlow());
        }
    }

    IEnumerator VoiceFlow()
    {
        // 先播放Tim语音
        if (timAudio != null && timAudio.clip != null)
        {
            timAudio.Play();
            yield return new WaitForSeconds(timAudio.clip.length);
        }
        // 再播放祖父前置对话
        if (grandpaTalkAudio != null && grandpaTalkAudio.clip != null)
        {
            grandpaTalkAudio.Play();
            yield return new WaitForSeconds(grandpaTalkAudio.clip.length);
        }
        // 弹出双按钮
        if (btnA) btnA.SetActive(true);
        if (btnB) btnB.SetActive(true);
    }

    // 绑定给按钮A（选项1）
    public void ChooseOptionOne()
    {
        HideButtons();
        StartCoroutine(Option1Logic());
    }

    // 绑定给按钮B（选项2）
    public void ChooseOptionTwo()
    {
        HideButtons();
        StartCoroutine(Option2Logic());
    }

    IEnumerator Option1Logic()
    {
        if (voiceOption1 != null && voiceOption1.clip != null)
        {
            voiceOption1.Play();
            yield return new WaitForSeconds(voiceOption1.clip.length);
        }
        // 语音播放完毕围栏消失
        if (fence != null)
            fence.SetActive(false);
    }

    IEnumerator Option2Logic()
    {
        if (voiceOption2 != null && voiceOption2.clip != null)
        {
            voiceOption2.Play();
            yield return new WaitForSeconds(voiceOption2.clip.length);
        }
        // 语音播放完毕围栏消失
        if (fence != null)
            fence.SetActive(false);
    }

    // 选中后隐藏两个按钮
    void HideButtons()
    {
        if (btnA) btnA.SetActive(false);
        if (btnB) btnB.SetActive(false);
    }
}