using UnityEngine;
using Leopotam.EcsLite;

public class Startup : MonoBehaviour
{
    [SerializeField]
    private SceneData _sceneData;

    private EcsWorld _world;
    private EcsSystems _systems;
    private SharedData _sharedData;

    private void Start()
    {
        _sharedData = new SharedData(new CustomTime(), _sceneData);
        _world = new EcsWorld();
        _systems = new EcsSystems(_world, _sharedData);

        SetupInput();

        _systems
            .Add(new PlayerMouseInputSystem())
            .Add(new CastScreenPositionToWorldPointSystem())
            .Add(new PlayerInitSystem())
            .Add(new PlayerMovementSystem())
            .Add(new PlayerRotationSystem())
            .Add(new PlayerViewUpdateSystem())
            .Add(new FloorButtonInitSystem())
            .Add(new FloorButtonSystem())
            .Add(new DoorInitSystem())
            .Add(new DoorOpeningSystem())
            .Add(new DoorViewUpdateSystem())
            .Init();
    }

    private void Update()
    {
        _sharedData?.Time.Update();
        _systems?.Run();
    }

    private void OnDestroy()
    {
        if (_systems != null)
        {
            _systems.Destroy();
            _systems = null;
        }

        if (_world != null)
        {
            _world.Destroy();
            _world = null;
        }
    }
    private void SetupInput()
    {
        var entity = _world.NewEntity();
        var pool = _world.GetPool<MouseInputComponent>();
        pool.Add(entity);
    }
}
