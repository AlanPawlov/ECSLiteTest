using UnityEngine;
using Leopotam.EcsLite;

public class PlayerMouseInputSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _filter;
    private EcsWorld _world;
    private EcsPool<MouseInputComponent> _playerInputComponentPool;

    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld();
        _filter = _world.Filter<MouseInputComponent>().End();
        _playerInputComponentPool = _world.GetPool<MouseInputComponent>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            ref var playerInputComponent = ref _playerInputComponentPool.Get(entity);
            playerInputComponent.IsButtonPressed = Input.GetMouseButtonDown(0);
            playerInputComponent.MousePosition = Input.mousePosition;
        }
    }
}
