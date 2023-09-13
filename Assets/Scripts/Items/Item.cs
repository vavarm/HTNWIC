using UnityEngine;

namespace HTNWIC.Items
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "HTNWIC/Item")]
    public class Item : ScriptableObject
    {
        public Sprite icon;
    }
}
