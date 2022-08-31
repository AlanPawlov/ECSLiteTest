using Leopotam.EcsLite;
using UnityEngine;

public class PlayerMovementSystem : IEcsRunSystem, IEcsInitSystem
{
    private EcsWorld _world;
    private EcsFilter _filter;
    private EcsPool<MovementComponent> _movementComponentPool;
    private EcsPool<TransformComponent> _transformComponentPool;

    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld();
        _filter = _world.Filter<MovementComponent>().Inc<TransformComponent>().End();
        _movementComponentPool = _world.GetPool<MovementComponent>();
        _transformComponentPool = _world.GetPool<TransformComponent>();
    }

    public void Run(IEcsSystems systems)
    {
        var deltaTime = systems.GetShared<SharedData>().Time.DeltaTime;
        foreach (var entity in _filter)
        {
            ref var movementComponent = ref _movementComponentPool.Get(entity);
            ref var transformComponent = ref _transformComponentPool.Get(entity);
            if (transformComponent.Position != movementComponent.TargetPosition)
            {
                transformComponent.Position =
                    Vector3.MoveTowards(transformComponent.Position, movementComponent.TargetPosition, movementComponent.Speed * deltaTime);
            }
        }
    }
}
