using UniMob.UI;
using UniMob.UI.Widgets;
using UnityEngine;

namespace Samples.BackButtonSupport
{
    public static class DemoUtils
    {
        public static Widget Text(Color color, params string[] texts)
        {
            return new PaddingBox(new RectPadding(10, 10, 10, 10))
            {
                Child = new Container
                {
                    BackgroundColor = color,
                    Child = new UniMobText
                    {
                        Value = string.Join("\n\n", texts),
                        FontSize = 50,
                        MainAxisAlignment = MainAxisAlignment.Center,
                        CrossAxisAlignment = CrossAxisAlignment.Center,
                    },
                }
            };
        }
    }
}