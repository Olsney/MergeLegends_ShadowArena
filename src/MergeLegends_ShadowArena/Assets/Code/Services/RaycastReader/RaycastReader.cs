using System;
using Code.Services.Input;
using UnityEngine;
using Zenject;

namespace Code.Services.RaycastReader
{
    public class RaycastReader : IDisposable, IInitializable
    {
        private readonly IInputService _inputService;

        public RaycastReader(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void Initialize()
        {
            _inputService.OnRay += Read;
        }

        private void Read(Ray ray)
        {
            Debug.Log("Raycast Hit");

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                //var soldierHit = hit.collider.GetComponent<Soldier>();
            }
        }

        public void Dispose()
        {
            _inputService.OnRay -= Read;
        }
    }
}