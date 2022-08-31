using UnityEngine;
using Leopotam.EcsLite;

public class FloorButtonSystem : IEcsRunSystem, IEcsInitSystem
{
    private EcsWorld _world;
    private EcsFilter _movementFilter;
    private EcsFilter _buttonFilter;
    private EcsFilter _doorFilter;
    private EcsPool<FloorButtonComponent> _floorComponentPool;
    private EcsPool<DoorComponent> _doorComponentPool;
    private EcsPool<TransformComponent> _transformComponentPool;

    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld();
        _movementFilter = _world.Filter<MovementComponent>().Inc<TransformComponent>().End();
        _buttonFilter = _world.Filter<FloorButtonComponent>().Inc<TransformComponent>().End();
        _doorFilter = _world.Filter<DoorComponent>().End();
        _floorComponentPool = _world.GetPool<FloorButtonComponent>();
        _doorComponentPool = _world.GetPool<DoorComponent>();
        _transformComponentPool = _world.GetPool<TransformComponent>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var movalable in _movementFilter)
        {
            var movementTransformComponent = _transformComponentPool.Get(movalable);
            foreach (var button in _buttonFilter)
            {
                var floorTransformComponent = _transformComponentPool.Get(button);
                var floorComponent = _floorComponentPool.Get(button);
                var distance = Vector3.Distance(floorTransformComponent.Position, movementTransformComponent.Position);
                foreach (var door in _doorFilter)
                {
                    ref var doorComponent = ref _doorComponentPool.Get(door);
                    if (distance < floorComponent.Radius)
                    {
                        if (doorComponent.DoorState == DoorState.Close && doorComponent.Id == floorComponent.Id)
                        {
                            doorComponent.DoorState = DoorState.InProcess;
                        }
                        continue;
                    }

                    if (doorComponent.DoorState == DoorState.InProcess && doorComponent.Id == floorComponent.Id)
                    {
                        doorComponent.DoorState = DoorState.Close;
                    }
                }
            }
        }
    }
}
