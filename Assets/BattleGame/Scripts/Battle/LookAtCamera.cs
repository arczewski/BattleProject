using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview
{
    public class LookAtCamera : MonoBehaviour
    {
        [SerializeField] private Camera camera;

        private void Awake()
        {
            if(camera == null)
                camera = Camera.main;
        }
        void Update()
        {
            transform.rotation = camera.transform.rotation;
        }
    }
}
