using UniMob.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.SimpleTodoList.Views
{
    public class TodoView : View<ITodoState>
    {
        [SerializeField] private Text todoText = default;

        [SerializeField] private Toggle completedToggle = default;
        [SerializeField] private Button deleteButton = default;

        [SerializeField] private Color activeTextColor = Color.black;
        [SerializeField] private Color completedTextColor = Color.gray;

        protected override void Awake()
        {
            base.Awake();

            deleteButton.Click(() => State.Delete);

            completedToggle.onValueChanged.Bind(completed => State.Completed = completed);
        }

        protected override void Render()
        {
            todoText.color = State.Completed ? completedTextColor : activeTextColor;
            todoText.text = State.Text;

            completedToggle.isOn = State.Completed;
        }
    }

    public interface ITodoState : IViewState
    {
        string Text { get; }
        bool Completed { get; set; }

        void Delete();
    }
}