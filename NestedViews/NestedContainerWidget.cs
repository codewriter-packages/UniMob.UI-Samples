using UniMob;
using UniMob.UI;

namespace Samples.NestedViews
{
    public class NestedContainerWidget : StatefulWidget
    {
    }

    public class NestedContainerState : ViewState<NestedContainerWidget>, INestedContainerState
    {
        public NestedContainerState(WidgetViewReference view)
        {
            View = view;
        }

        public override WidgetViewReference View { get; }

        [Atom] public INestedState NestedState => RenderChildT(_ => new NestedWidget
        {
            Message = "Hello from nested widget"
        }).As<NestedState>();
    }
}