using System.Linq;
using UniMob;
using UniMob.UI;
using UniMob.UI.Widgets;
using UnityEngine;

namespace Samples.SimpleTodoList.Presentation
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

        [Atom] public TodoFilterMode FilterMode { get; set; } = TodoFilterMode.All;
        [Atom] public bool CanClearCompletedTodos => _todoList.FinishedTodoCount != 0;
        [Atom] public int TodosLeft => _todoList.UnfinishedTodoCount;
        [Atom] public IState Todos => _todosState.Value;

        public void AddTodo(string text)
        {
            _todoList.AddTodo(text);
        }

        public void ClearCompletedTodos()
        {
            _todoList.RemoveCompletedTodos();
        }

        private Widget BuildTodos(BuildContext context)
        {
            if (_todoList.Todos.Length == 0)
            {
                return new UniMobText(WidgetSize.Stretched)
                {
                    Value = "No todos",
                    Color = Color.gray,
                    FontSize = 30,
                    MainAxisAlignment = MainAxisAlignment.Center,
                    CrossAxisAlignment = CrossAxisAlignment.Center,
                };
            }

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