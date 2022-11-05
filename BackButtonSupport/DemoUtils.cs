using UniMob.UI;
using UniMob.UI.Widgets;

namespace Samples.BackButtonSupport
{
    public static class DemoUtils
    {
        public static Widget Text(params string[] texts)
        {
            return new UniMobText(WidgetSize.Stretched)
            {
                Value = string.Join("\n\n", texts),
                FontSize = 50,
                MainAxisAlignment = MainAxisAlignment.Center,
                CrossAxisAlignment = CrossAxisAlignment.Center,
            };
        }
    }
}