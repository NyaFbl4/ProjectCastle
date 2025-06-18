using UnityEngine;

namespace Assets.Project.Scripts.Systems.Building
{
    public class Building : MonoBehaviour
    {
        [SerializeField] private Vector2Int _sizeVector = Vector2Int.one;
        [SerializeField] private Renderer _mainRenderer;

        //public Renderer MainRenderer => _mainRenderer;
        public Vector2Int SizeVector => _sizeVector;

        public void SetTransparent(bool available)
        {
            if (available)
            {
                _mainRenderer.material.color = Color.green;
            }
            else
            {
                _mainRenderer.material.color = Color.red;
            }
        }

        public void SetNormalColor()
        {
            _mainRenderer.material.color = Color.white;
        }
        
         void OnDrawGizmosSelected()
        {
            for (int x = 0; x < _sizeVector.x; x++)
            {
                for (int y = 0; y < _sizeVector.y; y++)
                {
                    if ((x + y) % 2 == 0)
                    {
                        Gizmos.color = Color.red;
                    }
                    else
                    {
                        Gizmos.color = Color.green;    
                    }
                    
                    Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1,0.1f,1));
                } 
            }
        }
    }
}