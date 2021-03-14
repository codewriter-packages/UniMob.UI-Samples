using UniMob;
using UniMob.UI;
using UniMob.UI.Widgets;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.SimpleTodoList.Presentation
{
    public class TodoListView : View<ITodoListState>
    {
        [SerializeField] private Button addTodoButton = default;
        [SerializeField] private Button allTodosButton = default;
        [SerializeField] private Button activeTodosButton = default;
        [SerializeField] private Button completedTodosButton = default;
        [SerializeField] private Button clearCompletedButton = default;
        [SerializeField] private Text itemsLeftText = default;
        [SerializeField] private InputField newTodoInputField = default;
        [SerializeField] private ViewPanel todosViewPanel = default;

        [Atom] private string CurrentNewTodoText { get; set; }

        protected override void Awake()
        {
            base.Awake();

            addTodoButton.Click(() => AddTodo);
            allTodosButton.Click(() => State.FilterMode = TodoFilterMode.All);
            activeTodosButton.Click(() => State.FilterMode = TodoFilterMode.Active);
            completedTodosButton.Click(() => State.FilterMode = TodoFilterMode.Completed);
            clearCompletedButton.Click(() => State.ClearCompletedTodos);
            newTodoInputField.onValueChanged.AddListener(text => CurrentNewTodoText = text);
        }

        protected override void Render()
        {
            itemsLeftText.text = $"Items left: {State.TodosLeft}";

            addTodoButton.interactable = !string.IsNullOrWhiteSpace(CurrentNewTodoText);

            allTodosButton.interactable = State.FilterMode != TodoFilterMode.All;
            activeTodosButton.interactable = State.FilterMode != TodoFilterMode.Active;
            completedTodosButton.interactable = State.FilterMode != TodoFilterMode.Completed;

            clearCompletedButton.gameObject.SetActive(State.CanClearCompletedTodos);

            todosViewPanel.Render(State.Todos, true);
        }

        private void AddTodo()
        {
            var text = newTodoInputField.text;
            newTodoInputField.text = string.Empty;

            State.AddTodo(text);
        }
    }

    public interface ITodoListState : IViewState
    {
        int TodosLeft { get; }
        bool CanClearCompletedTodos { get; }
        TodoFilterMode FilterMode { get; set; }
        IState Todos { get; }

        void AddTodo(string text);
        void ClearCompletedTodos();
    }

    public enum TodoFilterMode
    {
        All,
        Active,
        Completed,
    }
}