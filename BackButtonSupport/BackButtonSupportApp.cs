using UniMob.UI;
using UniMob.UI.Widgets;
using UnityEngine;

namespace Samples.BackButtonSupport
{
    public class BackButtonSupportApp : UniMobUIApp
    {
        private readonly BackButtonController _backButtonController = new BackButtonController();

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _backButtonController.HandleBack();
            }
        }

        protected override Widget Build(BuildContext context)
        {
            return new AppWidget(_backButtonController);
        }
    }
}