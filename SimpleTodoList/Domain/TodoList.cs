using System;
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

        public Todo GetTodoById(Guid id)
        {
            return Todos.First(todo => todo.Id == id);
        }

        public void AddTodo(string text)
        {
            var newTodoId = Guid.NewGuid();
            var newTodo = new Todo(Lifetime, newTodoId)
            {
                Title = text
            };

            Todos = Todos.Append(newTodo).ToArray();
        }

        public void RemoveTodoById(Guid id)
        {
            Todos = Todos.Where(todo => todo.Id != id).ToArray();
        }

        public void RemoveFinishedTodos()
        {
            Todos = Todos.Where(todo => !todo.Finished).ToArray();
        }
    }
}