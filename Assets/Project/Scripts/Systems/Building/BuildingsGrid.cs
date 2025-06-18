using UnityEngine;

namespace Assets.Project.Scripts.Systems.Building
{
    public class BuildingsGrid : MonoBehaviour
    {
        [SerializeField] private Vector2Int _gridSize = new Vector2Int(10, 10);
        
        [SerializeField] private Color _gridColor = Color.white;

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
            _grid = new Building[_gridSize.x, _gridSize.y];
            
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
                    int y = Mathf.RoundToInt(worldPosition.z);

                    bool available = true;

                    if (x < 0 || x > _gridSize.x - _flyingBuilding.SizeVector.x) available = false;
                    if (y < 0 || y > _gridSize.y - _flyingBuilding.SizeVector.y) available = false;

                    _flyingBuilding.transform.position = new Vector3(x, 0 ,  y);
                    _flyingBuilding.SetTransparent(available);
                     
                    if (available && Input.GetMouseButtonDown(0))
                    {
                        _flyingBuilding.SetNormalColor();
                        _flyingBuilding = null;
                    }
                }
            }
        }
        
    private void OnDrawGizmos()
    {
        Gizmos.color = _gridColor;
        
        // Рисуем вертикальные линии
        for (int x = 0; x <= _gridSize.x; x++)
        {
            Gizmos.DrawLine(
                new Vector3(x, 0, 0),
                new Vector3(x, 0, _gridSize.y)
            );
        }
        
        // Рисуем горизонтальные линии
        for (int y = 0; y <= _gridSize.y; y++)
        {
            Gizmos.DrawLine(
                new Vector3(0, 0, y),
                new Vector3(_gridSize.x, 0, y)
            );
        }
    }
    }
}