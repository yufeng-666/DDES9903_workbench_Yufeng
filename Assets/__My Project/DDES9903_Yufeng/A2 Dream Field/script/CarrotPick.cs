using UnityEngine;

public class CarrotPick : MonoBehaviour
{
    // 拖场景里的蔬菜盒子
    public VegetableBox vegetableBox;

    // 被点击时执行拾取
    public void PickCarrot()
    {
        if (vegetableBox != null)
        {
            vegetableBox.AddVegetable();
        }
        Destroy(gameObject); // 胡萝卜直接消失，代表捡走放进筐里
    }
}