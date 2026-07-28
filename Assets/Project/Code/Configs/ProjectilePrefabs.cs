using UnityEngine;

namespace Project.Code.Configs
{
    [CreateAssetMenu(fileName = "ProjectilesConfig", menuName = "Configs/BulletConfig", order = 51)]
    public class ProjectilePrefabs : ScriptableObject
    {
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private GameObject _laserPrefab;
        
        public GameObject BulletPrefab => _bulletPrefab;
        public GameObject LaserPrefab => _laserPrefab;
    }
}