using System.Linq;
using UniMob;

namespace Samples.SimpleTodoList
{
    public class TodoList : ILifetimeScope
    {
        public TodoList(Lifetime lifetime)
        {
            Lifetime = lifetime;
        }

        public Lifetime Lifetime { get; }

        [Atom] public Todo[] Todos { get; private set; } = new Todo[0];

        [Atom] public int UnfinishedTodoCount => Todos.Count(t => !t.Finished);

        [Atom] public int FinishedTodoCount => Todos.Count(t => t.Finished);

        public void AddTodo(string text)
        {
            Todos = Todos.Append(new Todo(Lifetime) {Title = text}).ToArray();
        }

        public void RemoveTodo(Todo todo)
        {
            Todos = Todos.Where(it => it != todo).ToArray();
        }

        public void RemoveFinishedTodos()
        {
            Todos = Todos.Where(it => !it.Finished).ToArray();
        }
    }
}