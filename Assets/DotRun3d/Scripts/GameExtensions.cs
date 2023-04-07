using Leopotam.EcsLite;

namespace DonRun3D
{
    public static class GameExtensions
    {
        public static void Delete<T>(this EcsFilter filter, EcsPool<T> pool) where T : struct
        {
            foreach (int entity in filter)
                pool.Del(entity);
        }
    }
}
