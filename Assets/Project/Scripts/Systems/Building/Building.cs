using UnityEngine;

namespace Assets.Project.Scripts.Systems.Building
{
    public class Building : MonoBehaviour
    {
        [SerializeField] private  Vector2Int Size = Vector2Int.one;

        
         void OnDrawGizmosSelected()
        {
            for (int x = 0; x < Size.x; x++)
            {
                for (int y = 0; y < Size.y; y++)
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