using System.Linq;
using Samples.SimpleTodoList.Views;
using UniMob;
using UniMob.UI;
using UniMob.UI.Widgets;

namespace Samples.SimpleTodoList.Widgets
{
    public class TodoListWidget : StatefulWidget
    {
        public override State CreateState() => StateProvider.Of(this);
    }

    public class TodoListState : ViewState<TodoListWidget>, ITodoListState
    {
        private readonly TodoList _todoList;
        private readonly StateHolder _todosState;

        public TodoListState(WidgetViewReference view, TodoList todoList)
        {
            View = view;

            _todoList = todoList;
            _todosState = CreateChild(BuildTodos);
        }

        public override WidgetViewReference View { get; }

        [Atom] public string NewTodoText { get; set; } = string.Empty;
        [Atom] public bool CanAddNewTodo => !string.IsNullOrWhiteSpace(NewTodoText);
        [Atom] public TodoFilterMode FilterMode { get; set; } = TodoFilterMode.All;
        [Atom] public bool CanClearCompletedTodos => _todoList.FinishedTodoCount != 0;
        [Atom] public int TodosLeft => _todoList.UnfinishedTodoCount;
        [Atom] public IState Todos => _todosState.Value;

        public void AddNewTodo()
        {
            if (!CanAddNewTodo)
            {
                return;
            }

            var text = NewTodoText;
            NewTodoText = string.Empty;

            _todoList.AddTodo(text);
        }

        public void ClearCompletedTodos()
        {
            if (!CanClearCompletedTodos)
            {
                return;
            }

            _todoList.RemoveFinishedTodos();
        }

        private Widget BuildTodos(BuildContext context)
        {
            return new ScrollList
            {
                MainAxisAlignment = MainAxisAlignment.Start,
                CrossAxisAlignment = CrossAxisAlignment.Center,
                Children =
                {
                    _todoList.Todos.Where(IsTodoVisible).Select(BuildTodo)
                }
            };
        }

        private bool IsTodoVisible(Todo todo)
        {
            switch (FilterMode)
            {
                case TodoFilterMode.Active: return !todo.Finished;
                case TodoFilterMode.Completed: return todo.Finished;
                default: return true;
            }
        }

        private Widget BuildTodo(Todo todo)
        {
            return new TodoWidget(todo)
            {
                Key = Key.Of(todo)
            };
        }
    }
}