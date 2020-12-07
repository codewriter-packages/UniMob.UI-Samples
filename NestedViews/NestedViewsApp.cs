using UniMob.UI;
using UnityEngine;

namespace Samples.NestedViews
{
    public class NestedViewsApp : UniMobUIApp
    {
        [SerializeField] private string nestedContainerView = default;
        [SerializeField] private string nestedView = default;

        protected override void Initialize()
        {
            base.Initialize();

            StateProvider.Register<NestedContainerWidget>(() => new NestedContainerState(
                WidgetViewReference.Resource(nestedContainerView)
            ));
            StateProvider.Register<NestedWidget>(() => new NestedState(
                WidgetViewReference.Resource(nestedView)
            ));
        }

        protected override Widget Build(BuildContext context)
        {
            return new NestedContainerWidget();
        }
    }
}