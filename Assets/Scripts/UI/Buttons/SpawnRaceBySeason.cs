using UnityEngine;

namespace Racing
{
    public class SpawnRaceBySeason : SpawnObjectByPropertiesList
    {
        [SerializeField] private Season season;

        protected override void ApplyProperties()
        {
            for (int i = 0; i < season.RaceInfos.Length; i++)
            {
                GameObject gameObject = Instantiate(prefab, parent);
                IScriptableObjectProperty scriptableObjectProperty = gameObject.GetComponent<IScriptableObjectProperty>();
                scriptableObjectProperty.ApplyProperty(season.RaceInfos[i]);
            }
        }
    }
}

