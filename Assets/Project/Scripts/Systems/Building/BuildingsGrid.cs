using UnityEngine;

namespace Assets.Project.Scripts.Systems.Building
{
    public class BuildingsGrid : MonoBehaviour
    {
        [SerializeField] private Vector2Int GridSize = new Vector2Int(10, 10);

        private Building[,] _grid;

        private Building _flyingBuilding;

        private Camera _mainCamera;
        
        public void StartsBuilding(Building buildingPrefab)
        {
            if (_flyingBuilding != null)
            {
                Destroy(_flyingBuilding);
            }

            _flyingBuilding = Instantiate(buildingPrefab);
        }
        
        private void Awake()
        {
            _grid = new Building[GridSize.x, GridSize.y];
            
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (_flyingBuilding != null)
            {
                var groundPlane = new Plane(Vector3.up, Vector3.zero);
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

                if (groundPlane.Raycast(ray, out float position))
                {
                    Vector3 worldPosition = ray.GetPoint(position);

                    int x = Mathf.RoundToInt(worldPosition.x);
                    int y = Mathf.RoundToInt(worldPosition.y);
                    
                    _flyingBuilding.transform.position = new Vector3(x, 0 ,  y);
                }
            }
        }
    }
}