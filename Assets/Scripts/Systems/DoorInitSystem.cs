using Leopotam.EcsLite;
using UnityEngine;

public class DoorInitSystem : IEcsInitSystem
{
    public void Init(IEcsSystems systems)
    {
        var _world = systems.GetWorld();
        var sceneData = systems.GetShared<SharedData>().SceneData;

        var doorTransforms = new Transform[sceneData.Doors.Length];
        for (int i = 0; i < sceneData.Doors.Length; i++)
        {
            var entity = _world.NewEntity();
            var pool = _world.GetPool<DoorComponent>();
            var transformPool = _world.GetPool<TransformComponent>();
            var doorViewPool = _world.GetPool<DoorsViewComponent>();

            ref var doorComponent = ref pool.Add(entity);
            ref var transformComponent = ref transformPool.Add(entity);
            ref var doorViewComponent = ref doorViewPool.Add(entity);

            doorComponent.Id = sceneData.Doors[i].Id;
            doorTransforms[i] = sceneData.Doors[i].Transform;
            doorViewComponent.DoorsTransform = doorTransforms;
            doorComponent.PositionInClosedState = sceneData.Doors[i].PositionInClosedState;
            doorComponent.PositionInOpenedState = sceneData.Doors[i].PositionInOpenedState;
            transformComponent.Position = doorViewComponent.DoorsTransform[i].position;
            doorComponent.DoorState = DoorState.Close;
        }
    }
}
