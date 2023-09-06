using UnityEngine;
using UnityEngine.UI;

namespace HTNWIC.Items
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "HTNWIC/Item")]
    public class Item : ScriptableObject
    {
        public Image icon;
    }
}
