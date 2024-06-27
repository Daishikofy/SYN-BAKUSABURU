using UnityEngine;

namespace Components
{
    public class MovementComponent : MonoBehaviour
    {
        public float WalkSpeed = 3.0f;

        public Rigidbody Rigidbody { get; private set; }
        public Vector3 MovementDirection { get; private set; }

        private float _currentSpeed;


        // Start is called before the first frame update
        void Start()
        {
            Rigidbody = GetComponent<Rigidbody>();
            WalkSpeed /= 10.0f;
            _currentSpeed = WalkSpeed;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            var rbPosition = Rigidbody.position + MovementDirection * (_currentSpeed / Rigidbody.mass);
            Rigidbody.MovePosition(rbPosition);
        }

        public Vector3 GetMovementDirection()
        {
            return MovementDirection;
        }
        public void SetMovementDirection(Vector3 movementDirection)
        {
            MovementDirection = movementDirection.normalized;
        }
    }
}