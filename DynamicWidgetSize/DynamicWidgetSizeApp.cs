using System.Linq;
using UniMob.UI;
using UniMob.UI.Widgets;
using UnityEngine;

namespace Samples.DynamicWidgetSize
{
    public class DynamicWidgetSizeApp : UniMobUIApp
    {
        [SerializeField] private GameObject dynamicViewPrefab;

        protected override void Initialize()
        {
            StateProvider.Register<DynamicWidget>(() => new DynamicState(
                WidgetViewReference.FromPrefab(dynamicViewPrefab)
            ));

            base.Initialize();
        }

        protected override Widget Build(BuildContext context)
        {
            return new ScrollGridFlow
            {
                MaxCrossAxisCount = 1,
                CrossAxisAlignment = CrossAxisAlignment.Center,
                Children =
                {
                    Enumerable.Repeat<Widget>(new DynamicWidget(), 20),
                },
            };
        }
    }
}