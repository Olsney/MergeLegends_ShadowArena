using UnityEngine;

namespace Code.Services.Input
{
    public class RaycastReader
    {
        private IInputService _inputService;

        public RaycastReader(IInputService inputService)
        {
            _inputService = inputService;
            _inputService.OnRay += Read;
        }

        public void Read(Ray ray)
        {
            Debug.Log("Raycast Hit");

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                //var carHit = hit.collider.GetComponent<Vehicle>();
            }
        }
    }
}