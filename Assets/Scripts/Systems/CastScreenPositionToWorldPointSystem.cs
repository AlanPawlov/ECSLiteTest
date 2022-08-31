using UnityEngine;
using Leopotam.EcsLite;

public class CastScreenPositionToWorldPointSystem : IEcsRunSystem, IEcsInitSystem
{
    private EcsWorld _world;
    private EcsFilter _mouseInputFilter;
    private EcsPool<MouseInputComponent> _mouseInputComponentPool;
    private EcsFilter _movableFilter;
    private EcsPool<MovementComponent> _movementComponentPool;

    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld();
        _mouseInputFilter = _world.Filter<MouseInputComponent>().End();
        _mouseInputComponentPool = _world.GetPool<MouseInputComponent>();
        _movableFilter = _world.Filter<MovementComponent>().End();
        _movementComponentPool = _world.GetPool<MovementComponent>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var mouseInput in _mouseInputFilter)
        {
            var mouseInputComponent = _mouseInputComponentPool.Get(mouseInput);
            if (mouseInputComponent.IsButtonPressed)
            {
                if (!CastRayFromScreenPoint(mouseInputComponent.MousePosition, out var hitInfo))
                {
                    continue;
                }

                foreach (var e in _movableFilter)
                {
                    ref var movementComponent = ref _movementComponentPool.Get(e);
                    movementComponent.TargetPosition = hitInfo.point;
                }
            }
        }
    }

    public bool CastRayFromScreenPoint(Vector3 position, out RaycastHit hitInfo)
    {
        var camera = Camera.main;
        var ray = camera.ScreenPointToRay(position);
        return Physics.Raycast(ray, out hitInfo, Mathf.Infinity, Physics.DefaultRaycastLayers);
    }

}
