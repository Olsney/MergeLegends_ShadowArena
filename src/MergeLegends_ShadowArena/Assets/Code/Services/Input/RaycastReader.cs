using UnityEngine;

namespace Code.Services.Input
{
    public class RaycastReader
    {
        private IInputService _inputService;

        public RaycastReader(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void Read(Ray ray)
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                //var carHit = hit.collider.GetComponent<Vehicle>();
                Debug.Log("Raycast Hit");
            }
        }
    }
}