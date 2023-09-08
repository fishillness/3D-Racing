using UnityEngine;

namespace Racing
{
    public class SeasonList : MonoBehaviour
    {
        [SerializeField] private Season[] seasons;

        public Season[] Seasons => seasons;
    }
}

