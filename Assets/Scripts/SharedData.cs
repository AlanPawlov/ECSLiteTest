public class SharedData
{
    private SceneData _sceneData;
    private CustomTime _time;
    public CustomTime Time => _time;
    public SceneData SceneData => _sceneData;

    public SharedData(CustomTime time, SceneData sceneData)
    {
        _sceneData = sceneData;
        _time = time;
    }
}
