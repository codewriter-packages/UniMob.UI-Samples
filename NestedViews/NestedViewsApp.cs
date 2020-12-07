using UniMob.UI;
using UnityEngine;

namespace Samples.NestedViews
{
    public class NestedViewsApp : UniMobUIApp
    {
        [SerializeField] private GameObject nestedContainerView = default;
        [SerializeField] private GameObject nestedView = default;

        protected override void Initialize()
        {
            base.Initialize();

            StateProvider.Register<NestedContainerWidget>(() => new NestedContainerState(
                WidgetViewReference.FromPrefab(nestedContainerView)
            ));
            StateProvider.Register<NestedWidget>(() => new NestedState(
                WidgetViewReference.FromPrefab(nestedView)
            ));
        }

        protected override Widget Build(BuildContext context)
        {
            return new NestedContainerWidget();
        }
    }
}