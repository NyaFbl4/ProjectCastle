using UnityEngine;

namespace Assets.Project.Scripts.Systems.Building
{
    public class BuildingsGrid : MonoBehaviour
    {
        public Vector2Int Size { get; protected set; }
        public float Step { get; private set; }

        [Header("Grid Visualization")]
        [SerializeField] private bool _drawGridGizmos = true;
        [SerializeField] private Color _firstGridColor = new(0.65f, 1f, 0.4f);
        [SerializeField] private Color _secondGridColor = new(1f, 0.34f, 0.26f);
        [SerializeField] private bool _showOccupiedCells = true;
        [SerializeField] private Color _occupiedCellColor = Color.red;
        [SerializeField] private Vector3 _gridOriginOffset = Vector3.zero;
        [SerializeField] private Vector2Int _gridSize = new Vector2Int(10, 10);
        [SerializeField] private float _cellSize = 1f;

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

                    _flyingBuilding.transform.position = new Vector3(x, 0, y);
                    _flyingBuilding.SetTransparent(available);

                    if (available && Input.GetMouseButtonDown(0))
                    {
                        _flyingBuilding.SetNormalColor();
                        _flyingBuilding = null;
                    }
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            var width = _gridSize.x * Step;
            var height = _gridSize.y * Step;

            for (var x = 0; x < _gridSize.x; x++)
            {
                for (var y = 0; y < _gridSize.y; y++)
                {
                    if ((x + y) % 2 == 0) Gizmos.color = _firstGridColor;
                    else Gizmos.color = _secondGridColor;

                    var startOffset = new Vector3((width / 2f) - (Step / 2), (height / 2f) - (Step / 2), 0f);
                    var startPos = transform.position - startOffset;
                    var offset = new Vector3(x * Step, y * Step, 0);

                    Gizmos.DrawCube(startPos + offset, new Vector3(Step, Step, 0.002f));
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (!_drawGridGizmos) return;

            // Рассчитываем начальную позицию с учетом origin
            Vector3 gridOrigin = transform.position + _gridOriginOffset;

            // Рисуем цветные квадраты
            for (int x = 0; x < _gridSize.x; x++)
            {
                for (int y = 0; y < _gridSize.y; y++)
                {
                    // Выбираем цвет в шахматном порядке
                    bool isAlternateColor = (x + y) % 2 == 0;
                    Gizmos.color = isAlternateColor ? _firstGridColor : _secondGridColor;

                    // Рассчитываем центр и размер квадрата
                    Vector3 center = gridOrigin + new Vector3(
                        x * _cellSize + _cellSize * 0.5f,
                        0,
                        y * _cellSize + _cellSize * 0.5f
                    );

                    Vector3 size = new Vector3(_cellSize, 0.01f, _cellSize);

                    // Рисуем квадрат
                    Gizmos.DrawCube(center, size);

                    // Если нужно показать занятые ячейки
                    if (_showOccupiedCells && _grid != null && _grid[x, y])
                    {
                        Gizmos.color = _occupiedCellColor;
                        Gizmos.DrawCube(center, size * 0.95f); // Чуть меньше основного квадрата
                    }
                }
            }
        }
    }
}