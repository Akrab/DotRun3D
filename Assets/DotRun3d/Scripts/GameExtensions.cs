using DonRun3D.ECS.LevelContructor;
using DonRun3D.ECS.Player;
using Leopotam.EcsLite;
using UnityEngine;

namespace DonRun3D
{
    public static class GameExtensions
    {
        public static void Delete<T>(this EcsFilter filter, EcsPool<T> pool) where T : struct
        {
            foreach (int entity in filter)
                pool.Del(entity);
        }

        public static void Delete<T>(this IEcsSystems systems) where T : struct
        {
            var filter = systems.GetWorld().Filter<T>().End();
            var pool = systems.GetWorld().GetPool<T>();
            filter.Delete(pool);
        }

        public static bool Have<T>(this IEcsSystems systems) where T : struct
        {
            var world = systems.GetWorld();
            var filter = world.Filter<T>().End();

            return filter.GetEntitiesCount() > 0;
        }

        public static int Count<T>(this IEcsSystems systems) where T : struct
        {
            var world = systems.GetWorld();
            var filter = world.Filter<T>().End();

            return filter.GetEntitiesCount();
        }

        public static void AddComp<T, D>(this IEcsSystems systems) where T : struct  where D : struct
        {
            var filter = systems.GetWorld().Filter<T>().End();
            var pool = systems.GetWorld().GetPool<D>();


            foreach (var item in filter)
                pool.Add(item);

        }

        public static void GetFirstComp<T>(this EcsWorld ecsWorld, out T res) where T : struct
        {
            var filter = ecsWorld.Filter<T>().End();
            var pool = ecsWorld.GetPool<T>();

            res = default(T);

            foreach (var item in filter)
            {
                ref T r = ref pool.Get(item);
                res = r;
                return;
            }

        }
    }
}
