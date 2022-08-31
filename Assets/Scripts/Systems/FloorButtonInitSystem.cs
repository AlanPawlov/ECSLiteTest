using Leopotam.EcsLite;

public class FloorButtonInitSystem : IEcsInitSystem
{
    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();
        var sceneData = systems.GetShared<SharedData>().SceneData;

        for (int i = 0; i < sceneData.FloorButtons.Length; i++)
        {
            var entity = world.NewEntity();
            var pool = world.GetPool<FloorButtonComponent>();
            var transformPool = world.GetPool<TransformComponent>();

            ref var floorButtonComponent = ref pool.Add(entity);
            ref var transform = ref transformPool.Add(entity);

            floorButtonComponent.Id = sceneData.FloorButtons[i].Id;
            transform.Position = sceneData.FloorButtons[i].Position;
            floorButtonComponent.Radius = sceneData.FloorButtons[i].Radius;
        }
    }
}
