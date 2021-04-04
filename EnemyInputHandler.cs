using System;
using InternalAssets.Scripts.Player;
using UnityEngine;

namespace InternalAssets.Scripts.Enemy
{
    public class EnemyInputHandler : MonoBehaviour
    {
        [SerializeField] private SpawnPoint _spawnPoint;
        private PlayerController _playerControl;
        private Vector3 _directionNorm;
        private float turnSpeed = 100;
        private float speed = 10;
        private Rigidbody2D _rb2D;

        public float RawRotationInput { get; private set; }
        public Vector3 DirectionNorm => _directionNorm;

        private void Start()
        {
            _playerControl = FindObjectOfType<PlayerController>();
            _rb2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            // vector from this object towards the target location
            Vector3 vectorToTarget = _playerControl.transform.position - transform.position;
            // rotate that vector by 90 degrees around the Z axis
            Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;
 
            // get the rotation that points the Z axis forward, and the Y axis 90 degrees away from the target
            // (resulting in the X axis facing the target)
            Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);
 
            // changed this from a lerp to a RotateTowards because you were supplying a "speed" not an interpolation value
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
            //transform.Translate(Vector3.right * speed * Time.deltaTime, Space.Self);
            _rb2D.AddRelativeForce( (Vector2.right * Time.deltaTime) / 100 );
            
        }
    }
}