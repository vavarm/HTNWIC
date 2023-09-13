using UnityEngine;

namespace HTNWIC
{
    public interface IInteractable
    {

        public string InteractionPrompt { get; }

        public void Interact(GameObject source);
    }
}
