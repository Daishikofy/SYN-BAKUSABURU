using UnityEngine;
using UnityEngine.Events;

namespace TOWER
{
    public class TOW_InteractableComponent : MonoBehaviour
    {
        public UnityEvent onInteraction;

        public void Interact()
        {
            onInteraction.Invoke();
        }
    }
}