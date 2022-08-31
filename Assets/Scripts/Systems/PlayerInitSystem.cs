using Leopotam.EcsLite;

public class PlayerInitSystem : IEcsInitSystem
{
    public void Init(IEcsSystems systems)
    {
        var sceneData = systems.GetShared<SharedData>().SceneData;
        var world = systems.GetWorld();
        var entity = world.NewEntity();
        var movementPool = world.GetPool<MovementComponent>();
        var transformPool = world.GetPool<TransformComponent>();
        var rotationPool = world.GetPool<RotationComponent>();
        var playerViewPool = world.GetPool<PlayerViewComponent>();

        ref var movement = ref movementPool.Add(entity);
        ref var transform = ref transformPool.Add(entity);
        ref var view = ref playerViewPool.Add(entity);
        rotationPool.Add(entity);

        view = sceneData.PlayerConfig.PlayerView;

        transform.Position = view.Transform.position;
        movement.TargetPosition = view.Transform.position;
        movement.Speed = sceneData.PlayerConfig.Speed;

    }
}
