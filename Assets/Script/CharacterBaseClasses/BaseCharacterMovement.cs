using UnityEngine;

namespace Project.BaseClasses 
{ 
    public class BaseCharacterMovement : MonoBehaviour
    {
        [SerializeField] private BaseCharacterAnimations baseCharacterAnimations;
        [SerializeField] private Rigidbody rb;

        [SerializeField] private float speed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float yAxisLimit;
        protected bool canMove = false;

        protected Vector3 moveVector;
        private void FixedUpdate()
        {
            if(moveVector != Vector3.zero && canMove)
            {
                rb.angularVelocity = Vector3.zero;
                Move();
                LookToMoveVector();
            }
        }
        protected virtual void Update()
        {
            if(canMove)
            {
                SetMoveVector();
                CheckCharacterFellDown();
            }
            SetAnimations();
        }
        private void LookToMoveVector()
        {
            if (moveVector != Vector3.zero)
                rb.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveVector), Time.fixedDeltaTime * rotationSpeed);
        }
        private void Move()
        {
            rb.MovePosition(rb.position + moveVector * speed * Time.fixedDeltaTime);
        }
        protected virtual void SetMoveVector()
        {
            Debug.LogError("Error: Not Setting Move Vector!");
        }
        private void SetAnimations()
        {
            baseCharacterAnimations.SetMoveMagnitude(moveVector.magnitude);
        }
        private void CheckCharacterFellDown()
        {
            if (transform.position.y < yAxisLimit)
                OnCharacterFellDown();
        }
        protected virtual void OnCharacterFellDown()
        {
            Debug.LogError("OnCharacterFellDown not implmeneted");
        }
        public void Stop()
        {
            canMove = false;
            moveVector = Vector3.zero;
        }
        public virtual void Resume() => canMove = true;
        public virtual void SetPoisiton(Vector3 pos)
        {
            transform.position = pos;
        }
    }
}
