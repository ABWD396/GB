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
        [SerializeField, Range(20, 100)]
        private float jumpForce = 20f;

        ParticleSystem ps;
        Rigidbody rb;
        Vector3 movement;
        bool jumpPressed = false;
        bool onGround = false;
        Transform transformPlayer;

        public LevelController levelController;

        void Start()
        {
            transformPlayer = GetComponent<Transform>();
            rb = GetComponent<Rigidbody>();
            ps = GetComponentInChildren<ParticleSystem>();
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
                    rb.AddForce(0, jumpForce, 0);
                }
                jumpPressed = false;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.tag.Equals("Ground"))
            {
                onGround = false;
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
                ps.Play();
                StartCoroutine(FinishDelay());
            }
        }

        private IEnumerator FinishDelay() {
            transformPlayer.localScale = Vector3.zero;
            rb.isKinematic = true;

            yield return new WaitForSeconds(1);

            levelController.FailLevel();
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