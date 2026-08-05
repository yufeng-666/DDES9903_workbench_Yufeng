using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GroundTriggerGrandpaDialog : MonoBehaviour
{
    [Header("踩地面触发：祖父第一段语音")]
    public AudioSource grandpaVoice1;

    [Header("两个选择按钮，Hierarchy里默认关闭")]
    public GameObject optionA;
    public GameObject optionB;

    [Header("选项对应的语音")]
    public AudioSource grandpaVoice2; // 选A播放
    public AudioSource grandpaVoice3; // 选B播放

    [Header("需要消失的物体")]
    public GameObject hideObjA; // A选项结束消失的物体（菜园围栏）
    public GameObject hideObjB; // B选项结束消失的物体（小狗围栏）

    private bool triggered = false;

    void Start()
    {
        if (optionA) optionA.SetActive(false);
        if (optionB) optionB.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 仅第一次踩到、只有玩家触发
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(DialogFlow());
        }
    }

    // 1.播放祖父第一段语音 → 弹出AB按钮
    IEnumerator DialogFlow()
    {
        if (grandpaVoice1 != null && grandpaVoice1.clip != null)
        {
            grandpaVoice1.Play();
            yield return new WaitForSeconds(grandpaVoice1.clip.length);
        }
        // 语音结束弹出选择按钮
        optionA.SetActive(true);
        optionB.SetActive(true);
    }

    // 绑定给按钮A
    public void SelectA()
    {
        HideButtons();
        StartCoroutine(LogicA());
    }

    // 绑定给按钮B
    public void SelectB()
    {
        HideButtons();
        StartCoroutine(LogicB());
    }

    IEnumerator LogicA()
    {
        // 播放祖父第二条语音
        if (grandpaVoice2 != null && grandpaVoice2.clip != null)
        {
            grandpaVoice2.Play();
            yield return new WaitForSeconds(grandpaVoice2.clip.length);
        }
        // 语音播完物体消失
        if (hideObjA != null)
            hideObjA.SetActive(false);
    }

    IEnumerator LogicB()
    {
        // 播放祖父第三条语音
        if (grandpaVoice3 != null && grandpaVoice3.clip != null)
        {
            grandpaVoice3.Play();
            yield return new WaitForSeconds(grandpaVoice3.clip.length);
        }
        if (hideObjB != null)
            hideObjB.SetActive(false);
    }

    void HideButtons()
    {
        optionA.SetActive(false);
        optionB.SetActive(false);
    }
}