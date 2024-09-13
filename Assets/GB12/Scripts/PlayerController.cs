using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GB12
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField, Range(0, 10)]
        private float speed = 1.0f;
        [SerializeField, Range(100, 1000)]
        private float jumpForce = 100.0f;

        public LevelController levelController;

        Rigidbody rb;

        Vector3 movement;

        bool jumpPressed = false;
        bool onGround = true;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            float hAxis = Input.GetAxis(GlobalStringVars.HORIZONTAL_AXIS);
            float vAxis = Input.GetAxis(GlobalStringVars.VERTICAL_AXIS);
            if (Input.GetButton(GlobalStringVars.JUMP_BUTTON))
            {
                jumpPressed = true;
            }

            movement = new Vector3(-hAxis, 0, -vAxis).normalized * speed;
        }

        private void FixedUpdate()
        {
            rb.AddForce(movement);

            if (jumpPressed)
            {
                if (onGround)
                {
                    onGround = false;
                    rb.AddForce(0, jumpForce, 0);
                }
                jumpPressed = false;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag.Equals("Ground"))
            {
                onGround = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag.Equals("Finish"))
            {
                levelController.FinishLevel();
            }
            else if (other.gameObject.tag.Equals("Respawn"))
            {
                levelController.FailLevel();
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Reset Values")]
        public void ResetValue()
        {
            speed = 1.0f;
        }
#endif
    }

}