using System;
using UniMob.UI;
using UniMob.UI.Widgets;
using UnityEngine;

namespace Samples.Navigation
{
    public class MainWidget : StatefulWidget
    {
        public Action ShowDetail { get; set; }

        public override State CreateState() => new MainState();
    }

    public class MainState : HocState<MainWidget>
    {
        public override Widget Build(BuildContext context)
        {
            return new Container
            {
                Size = WidgetSize.Stretched,
                BackgroundColor = Color.white,

                Child = new UniMobButton
                {
                    OnClick = () => Widget.ShowDetail?.Invoke(),

                    Child = new UniMobText(WidgetSize.Fixed(600, 200))
                    {
                        Value = "Open Detail",
                        FontSize = 60,
                        Color = Color.black,
                        MainAxisAlignment = MainAxisAlignment.Center,
                        CrossAxisAlignment = CrossAxisAlignment.Center,
                    }
                }
            };
        }
    }
}