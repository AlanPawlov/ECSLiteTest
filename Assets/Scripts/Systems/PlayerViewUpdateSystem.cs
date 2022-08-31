using Leopotam.EcsLite;

public class PlayerViewUpdateSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _filter;
    private EcsPool<MovementComponent> _movementComponentPool;
    private EcsPool<TransformComponent> _transformComponentPool;
    private EcsPool<RotationComponent> _rotationComponentPool;
    private EcsPool<PlayerViewComponent> _playerViewComponentPool;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();
        _filter = world.Filter<MovementComponent>().Inc<RotationComponent>()
            .Inc<TransformComponent>().Inc<PlayerViewComponent>().End();
        _movementComponentPool = world.GetPool<MovementComponent>();
        _transformComponentPool = world.GetPool<TransformComponent>();
        _rotationComponentPool = world.GetPool<RotationComponent>();
        _playerViewComponentPool = world.GetPool<PlayerViewComponent>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            var movementComponent = _movementComponentPool.Get(entity);
            var rotationComponent = _rotationComponentPool.Get(entity);
            var transformComponen = _transformComponentPool.Get(entity);
            ref var viewComponen = ref _playerViewComponentPool.Get(entity);
            viewComponen.Transform.position = transformComponen.Position;
            viewComponen.Transform.eulerAngles = rotationComponent.Rotation;
            viewComponen.Animator.SetBool(viewComponen.AnimatorTargetParameter, transformComponen.Position != movementComponent.TargetPosition);
        }
    }
}
