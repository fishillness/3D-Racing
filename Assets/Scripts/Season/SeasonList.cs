using UnityEngine;

namespace Racing
{
    public class SeasonList : MonoBehaviour
    {
        [SerializeField] private Season[] seasonsList;

        public Season[] SeasonsList => seasonsList;
    }
}

