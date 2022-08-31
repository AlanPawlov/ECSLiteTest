using UnityEngine;
using Leopotam.EcsLite;

public class PlayerRotationSystem : IEcsRunSystem, IEcsInitSystem
{
    private EcsWorld _world;
    private EcsFilter _filter;
    private EcsPool<MovementComponent> _movementComponentPool;
    private EcsPool<RotationComponent> _rotationComponentPool;
    private EcsPool<TransformComponent> _transformComponentPool;

    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld();
        _filter = _world.Filter<MovementComponent>().Inc<RotationComponent>().Inc<TransformComponent>().End();
        _movementComponentPool = _world.GetPool<MovementComponent>();
        _rotationComponentPool = _world.GetPool<RotationComponent>();
        _transformComponentPool= _world.GetPool<TransformComponent>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            ref var rotationComponent = ref _rotationComponentPool.Get(entity);
            var movementComponent = _movementComponentPool.Get(entity);
            var transformComponent = _transformComponentPool.Get(entity);

            if (transformComponent.Position != movementComponent.TargetPosition)
            {
                Rotate( ref rotationComponent, transformComponent, movementComponent);
            }
        }
    }

    private void Rotate(ref RotationComponent rotationComponent, TransformComponent transformComponent, MovementComponent movementComponent)
    {
        var playerToTargetDirection = (movementComponent.TargetPosition - transformComponent.Position).normalized;
        float angle = Mathf.Atan2(playerToTargetDirection.x, playerToTargetDirection.z) * Mathf.Rad2Deg;
        rotationComponent.Rotation = new Vector3(rotationComponent.Rotation.x, angle, rotationComponent.Rotation.z);
    }
}
