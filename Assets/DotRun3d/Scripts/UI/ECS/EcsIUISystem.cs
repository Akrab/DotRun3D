using Akrab.UI;
using Leopotam.EcsLite;
using System.Diagnostics.Eventing;

namespace DonRun3D.ECS.UI
{
    sealed class EPlayGameSystem : EBaseUISystem
    {
        public override void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var startGammeFilter = world.Filter<EPlayGameClickComp>().End();

            if (startGammeFilter.GetEntitiesCount() == 0)
                return;

            var pool = world.GetPool<EPlayGameClickComp>();

            foreach (int entity in startGammeFilter)
            {
                ref EPlayGameClickComp kz = ref pool.Get(entity);
                pool.Del(entity);
            }

            var mainForm = FindForm<MainUI>(systems);
            mainForm.Hide();
        }
    }

    public abstract class EBaseUISystem : IEcsRunSystem
    {

        protected T FindForm<T>(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<EFormComp>().End();

            if (filter.GetEntitiesCount() == 0)
                return default(T);

            var pool = world.GetPool<EFormComp>();

            foreach (var entity in filter)
            {
                ref EFormComp fc = ref pool.Get(entity);

                if (fc.form.GetType() == typeof(T))
                    return (T)(object)fc.form;
            }

            return default(T);
        }


        public abstract void Run(IEcsSystems systems);
    }
}
