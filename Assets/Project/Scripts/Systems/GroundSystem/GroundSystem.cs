using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSystem : MonoBehaviour
{
    [Header("Ground Settings")]
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private float _cellSize;
    [SerializeField] private Vector3 _originPosition;

    private bool[,] _gridArray;
    
    void Awake()
    {
        InitializeGrid();
    }

    // Инициализация сетки
    private void InitializeGrid()
    {
        _gridArray = new bool[_width, _height];
        
        // Заполняем сетку (true = занято, false = свободно)
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                _gridArray[x, y] = false; // По умолчанию все ячейки свободны
            }
        }
    }

    // Конвертация мировых координат в координаты сетки
    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - _originPosition).x / _cellSize);
        y = Mathf.FloorToInt((worldPosition - _originPosition).z / _cellSize); // Для 3D используем Z
    }

    // Проверка доступности ячейки
    public bool IsCellEmpty(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < _width && y < _height)
        {
            return !_gridArray[x, y];
        }
        return false;
    }

    // Изменение состояния ячейки
    public void SetCellState(int x, int y, bool state)
    {
        if (x >= 0 && y >= 0 && x < _width && y < _height)
        {
            _gridArray[x, y] = state;
        }
    }

    // Визуализация сетки в редакторе
    private void OnDrawGizmos()
    {
        if (_gridArray == null) return;

        Gizmos.color = Color.white;
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Vector3 cellCenter = GetWorldPosition(x, y) + new Vector3(_cellSize, 0, _cellSize) * 0.5f;
                Gizmos.DrawWireCube(cellCenter, new Vector3(_cellSize, 0.01f, _cellSize));
            }
        }
    }

    // Получение мировых координат ячейки
    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, 0, y) * _cellSize + _originPosition;
    }
}
