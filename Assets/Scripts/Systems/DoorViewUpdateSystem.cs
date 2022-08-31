using Leopotam.EcsLite;

public class DoorViewUpdateSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _filter;
    private EcsPool<TransformComponent> _transformComponentPool;
    private EcsPool<DoorComponent> _doorComponentPool;
    private EcsPool<DoorsViewComponent> _doorViewComponentPool;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();
        _filter = world.Filter<DoorComponent>().Inc<TransformComponent>().Inc<DoorsViewComponent>().End();
        _transformComponentPool = world.GetPool<TransformComponent>();
        _doorComponentPool = world.GetPool<DoorComponent>();
        _doorViewComponentPool = world.GetPool<DoorsViewComponent>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var door in _filter)
        {
            var transformComponent = _transformComponentPool.Get(door);
            var doorComponent = _doorComponentPool.Get(door);
            ref var doorViewComponent = ref _doorViewComponentPool.Get(door);
            doorViewComponent.DoorsTransform[doorComponent.Id].position = transformComponent.Position;
        }
    }
}
