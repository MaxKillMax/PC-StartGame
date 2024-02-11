using UnityEngine;

namespace SG.Locations
{
    public class LocationBuilder : MonoBehaviour
    {
        [SerializeField] private Transform _parent;

        private Location _createdLocation;

        public void Build(Location prefab)
        {
            if (_createdLocation)
                Destroy(_createdLocation.gameObject);

            _createdLocation = Instantiate(prefab, _parent);
        }
    }
}