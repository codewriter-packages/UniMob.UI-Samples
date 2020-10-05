using UniMob;
using UniMob.UI;
using UniMob.UI.Widgets;
using UnityEngine;

namespace Samples.HelloWorld
{
    public class SimpleCounterApp : UniMobUIApp
    {
        [Atom] private int Counter { get; set; } = 0;

        protected override Widget Build(BuildContext context)
        {
            return new Container
            {
                BackgroundColor = Color.cyan,
                Child = new UniMobButton
                {
                    OnClick = () => Counter += 1,
                    Child = new UniMobText(WidgetSize.Fixed(400, 100))
                    {
                        Value = "Tap count: " + Counter,
                        FontSize = 40,
                        MainAxisAlignment = MainAxisAlignment.Center,
                        CrossAxisAlignment = CrossAxisAlignment.Center,
                    }
                }
            };
        }
    }
}