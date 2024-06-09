public class BombGenerator : ObjectSpawner<Bomb>
{
    protected override void InitializeObject(Bomb @object)
    {
        @object.Init(this);
    }
}
