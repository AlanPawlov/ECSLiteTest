using UnityEngine;
using Leopotam.EcsLite;

public class DoorOpeningSystem : IEcsRunSystem, IEcsInitSystem
{
    private EcsWorld _world;
    private EcsFilter _doorFilter;
    private EcsPool<DoorComponent> _doorComponentPool;
    private EcsPool<TransformComponent> _transformComponentPool;

    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld();
        _doorFilter = _world.Filter<DoorComponent>().Inc<TransformComponent>().End();
        _doorComponentPool = _world.GetPool<DoorComponent>();
        _transformComponentPool = _world.GetPool<TransformComponent>();
    }

    public void Run(IEcsSystems systems)
    {
        var deltaTime = systems.GetShared<SharedData>().Time.DeltaTime;

        foreach (var door in _doorFilter)
        {
            ref var doorComponent = ref _doorComponentPool.Get(door);
            ref var transformComponent = ref _transformComponentPool.Get(door);
            if (doorComponent.DoorState == DoorState.InProcess)
            {
                var a = Vector3.Distance(doorComponent.PositionInOpenedState, doorComponent.PositionInClosedState);
                var b = Vector3.Distance(doorComponent.PositionInOpenedState, transformComponent.Position);
                var t = 1 - b / a;
                transformComponent.Position = Vector3.Lerp(doorComponent.PositionInClosedState, doorComponent.PositionInOpenedState, t + deltaTime);

                if (transformComponent.Position == doorComponent.PositionInOpenedState)
                {
                    doorComponent.DoorState = DoorState.Open;
                }
            }
        }
    }
}
