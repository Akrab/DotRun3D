using System.Collections.Generic;
using DonRun3D.ECS.LevelContructor;
using DonRun3D.ECS.Player;
using DonRun3D.World.Column;
using DotRun3d;
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

        public static void GetFirstComp<T>(this IEcsSystems systems, out T res) where T : struct
        {
            systems.GetWorld().GetFirstComp<T>(out res);
        }


        public static Material FindMaterial(this List<ColorMaterials> data, ColorType color)
        {
            return data.Find(D => D.color == color).material;
        }
        
        public static Material GetRandomMaterial(this List<ColorMaterials> data)
        {
            Dictionary<ColorType, int> weights = new Dictionary<ColorType, int>(data.Count);

            var w = 1000 / data.Count;

            for (int i = 0; i < data.Count; i++)
            {
                weights.Add(data[i].color, w);
            }

            var color = WeightedRandomizer.From(weights).TakeOne();

            return data.FindMaterial(color);
        }
    }
}
