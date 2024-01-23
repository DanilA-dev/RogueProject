using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class UnitLocomotion
{
    [SerializeField] private NavMeshAgent _navAgent;

    public NavMeshAgent NavAgent => _navAgent;

    public void MoveAgent(Vector3 pos) => _navAgent.SetDestination(pos);
    public void ToggleAgent(bool value) => _navAgent.enabled = value;
    public void StopAgent(bool value) => _navAgent.isStopped = value;
    public void ToggleRotation(bool value) => _navAgent.updateRotation = value;
}