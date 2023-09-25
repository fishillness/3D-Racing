using UnityEngine;

namespace Racing
{
    public class UIDeleteButton : MonoBehaviour
    {
        public void DeleteAllSaves()
        {
            Saves.DeleteAllSaves();
        }
    }
}

