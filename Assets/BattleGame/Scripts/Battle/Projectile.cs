using UnityEngine;
using UnityEngine.Events;

namespace AFSInterview
{
    public class Projectile : MonoBehaviour
    {
        public UnityEvent OnTargetHit;
        
        [SerializeField] private float speed;
        private Vector3 target;
        
        public void SetTarget(Vector3 targetWorldPosition)
        {
            target = targetWorldPosition;
        }
        
        private void Update()
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            if (Vector3.Distance(transform.position, target) < 0.001f)
            {
                OnTargetHit?.Invoke();
                OnTargetHit?.RemoveAllListeners();
                target = Vector3.zero;
                ObjectPool.Instance.ReturnObject(this.gameObject);
            }
        }
    }
}