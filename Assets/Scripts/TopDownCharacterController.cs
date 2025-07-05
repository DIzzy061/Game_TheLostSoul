using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    public class TopDownCharacterController : MonoBehaviour
    {
        public float speed;
        public bool freezeDirection = false;

        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            Vector2 dir = Vector2.zero;
            int direction = 0; // 0 - Down, 1 - Up, 2 - Right, 3 - Left

            if (!freezeDirection)
            {
                // Получаем ввод
                float horizontal = 0f;
                float vertical = 0f;
                if (Input.GetKey(KeyCode.A)) horizontal = -1f;
                if (Input.GetKey(KeyCode.D)) horizontal = 1f;
                if (Input.GetKey(KeyCode.W)) vertical = 1f;
                if (Input.GetKey(KeyCode.S)) vertical = -1f;

                dir = new Vector2(horizontal, vertical);

                // Определяем направление для анимации
                if (dir.x > 0)
                    direction = 2; // Right
                else if (dir.x < 0)
                    direction = 3; // Left
                else if (dir.y > 0)
                    direction = 1; // Up
                else if (dir.y < 0)
                    direction = 0; // Down

                animator.SetInteger("Direction", direction);
            }
            else
            {
                dir.x = Input.GetAxisRaw("Horizontal");
                dir.y = Input.GetAxisRaw("Vertical");
            }

            dir.Normalize();
            animator.SetBool("IsMoving", dir.magnitude > 0);

            GetComponent<Rigidbody2D>().velocity = speed * dir;
        }
    }
}
