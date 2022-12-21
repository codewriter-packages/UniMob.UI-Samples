using UniMob.UI;

namespace Samples.NestedViews
{
    public class NestedWidget : StatefulWidget
    {
        public string Message { get; set; }
    }

    public class NestedState : ViewState<NestedWidget>, INestedState
    {
        public NestedState(WidgetViewReference view)
        {
            View = view;
        }

        public override WidgetViewReference View { get; }

        public string Message => Widget.Message;
    }
}