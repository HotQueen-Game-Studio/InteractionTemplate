using UnityEngine;

namespace HotQueen.Interaction
{
    public class MouseInteractor : Interactor
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
            InputGrab(hitInfo);
            InputActivate();




        }

        private void InputActivate()
        {
            //Activate
            if (Input.GetMouseButtonDown(1))
            {
                Activate();
            }
            else if (Input.GetMouseButtonUp(1))
            {
                CancelActivate();
            }
        }

        private void InputGrab(RaycastHit hitInfo)
        {

            //Grab
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (hitInfo.collider.attachedRigidbody
                    && hitInfo.collider.attachedRigidbody.TryGetComponent<Grabbable>(out Grabbable grabbable))
                {
                    grabber.Grab(grabbable);
                }
            }
            else if (Input.GetKeyUp(KeyCode.G))
            {
                grabber.Drop();
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
                    Interact(interact);
                }


            }
            else if (Input.GetMouseButtonUp(0))
            {
                CancelInteract();
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
