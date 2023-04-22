using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace _Scripts.Companion
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class CompanionAI : MonoBehaviour
    {
        public Transform player;

        private NavMeshAgent _agent;

        private Vector3 _previousPosition;
        private float _previousTime;

        public float CurrentSpeed;

        private Transform _companionTransform;
        
        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _companionTransform = transform;
            _previousPosition = _companionTransform.position;
        }

        private void Update()
        {
            _agent.SetDestination(player.position);

            var distance = Mathf.Sqrt((transform.position - _previousPosition).sqrMagnitude);
            CurrentSpeed = distance / Time.deltaTime;
            
            var currentMovementF = Mathf.Min(.5f, CurrentSpeed / 4f);
            GetComponentInChildren<Animator>().SetFloat("Movement_f", currentMovementF);
            
            _previousPosition = _companionTransform.position;
        }
    }
}