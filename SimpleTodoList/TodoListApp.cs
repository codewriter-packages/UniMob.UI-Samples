using System.Linq;
using UniMob.UI;
using UniMob.UI.Widgets;
using UnityEngine;

namespace Samples.SimpleTodoList
{
    public class TodoListApp : UniMobUIApp
    {
        private readonly TodoList _store = new TodoList();

        protected override void Initialize()
        {
            _store.Todos = _store.Todos
                .Append(new Todo {Title = "Get Coffee"})
                .Append(new Todo {Title = "Write simpler code"})
                .ToArray();

            _store.Todos[0].Finished = true;
        }

        protected override Widget Build(BuildContext context)
        {
            return new Container
            {
                BackgroundColor = Color.white,
                Child = BuildTodoList(_store),
            };
        }

        private Widget BuildTodoList(TodoList todoList)
        {
            return new Column
            {
                MainAxisSize = AxisSize.Max,
                CrossAxisSize = AxisSize.Max,
                Children =
                {
                    todoList.Todos.Select(todo => BuildTodo(todo)),
                    new UniMobText(WidgetSize.FixedHeight(60))
                    {
                        Value = $"Tasks left: {todoList.UnfinishedTodoCount}",
                        FontSize = 50,
                    },
                }
            };
        }

        private Widget BuildTodo(Todo todo)
        {
            return new UniMobButton
            {
                OnClick = () => todo.Finished = !todo.Finished,
                Child = new UniMobText(WidgetSize.FixedHeight(60))
                {
                    Value = $" - {todo.Title}: {(todo.Finished ? "Finished" : "Active")}",
                    FontSize = 40,
                }
            };
        }
    }
}