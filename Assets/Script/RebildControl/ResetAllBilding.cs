using System.Collections.Generic;

namespace UnityEngine.InputSystem.Samples.RebindUI
{
    public class ResetAllBilding : MonoBehaviour
    {
        [SerializeField] private List<RebildControl> list;

        public void ResetDefealt()
        {
            foreach (var control in list)
            {
                control.ResetToDefault();
            }
        }
    }
}