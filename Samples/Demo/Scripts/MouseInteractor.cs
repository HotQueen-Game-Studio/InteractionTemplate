using UnityEngine;

namespace HotQueen.Interaction
{
    public class MouseInteractor : MonoBehaviour
    {
        public float distance = 1f;
        public Grabber grabber;
        public float moveSpeed = 1;

        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray.origin, ray.direction, out RaycastHit hitInfo, distance);
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.red, 1f);

            Move();
            InputInteraction(hitInfo);
            InputActivate();
        }

        private void InputActivate()
        {
            //Activate
            if (Input.GetMouseButtonDown(1))
            {
                grabber.Activate();
            }
            else if (Input.GetMouseButtonUp(1))
            {
                grabber.CancelActivate();
            }
        }

        private void InputInteraction(RaycastHit hitInfo)
        {
            //Interact
            if (Input.GetMouseButtonDown(0))
            {

                if (hitInfo.collider.attachedRigidbody
                    && hitInfo.collider.attachedRigidbody.TryGetComponent<InteractBase>(out InteractBase interact))
                {
                    grabber.Interact(interact);
                }


            }
            else if (Input.GetMouseButtonUp(0))
            {
                grabber.CancelInteract();
            }
        }

        private void Move()
        {
            Vector3 direction = new Vector3();
            direction.x = Input.GetAxis("Horizontal");
            direction.z = Input.GetAxis("Vertical");

            this.transform.position += direction * moveSpeed;
        }
    }
}
