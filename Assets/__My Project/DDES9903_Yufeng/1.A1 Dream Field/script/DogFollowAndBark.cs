using UnityEngine;

public class DogFollowAndTouchBark : MonoBehaviour
{
    [Header("拖拽你的玩家Player")]
    public Transform player;
    [Header("跟随距离")]
    public float followDistance = 2.2f;
    [Header("小狗移动速度")]
    public float moveSpeed = 1.8f;
    [Header("小狗触碰叫声音频")]
    public AudioClip dogBarkClip;

    private Transform dogTrans;
    private bool barked = false;

    void Awake()
    {
        dogTrans = transform;
    }

    void Update()
    {
        // 小狗自动跟随逻辑
        float dis = Vector3.Distance(dogTrans.position, player.position);
        if (dis > followDistance)
        {
            Vector3 dir = (player.position - dogTrans.position).normalized;
            dogTrans.position += dir * moveSpeed * Time.deltaTime;
            dogTrans.LookAt(new Vector3(player.position.x, dogTrans.position.y, player.position.z));
        }
    }

    // 小狗碰撞碰到玩家时触发
    void OnTriggerEnter(Collider other)
    {
        // 只识别玩家，只触发一次叫声
        if (!barked && other.CompareTag("Player") && dogBarkClip != null)
        {
            barked = true;
            AudioSource.PlayClipAtPoint(dogBarkClip, transform.position, 0.5f);
            Debug.Log("小狗碰到玩家，发出叫声");
        }
    }
}