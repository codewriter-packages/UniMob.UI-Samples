using System;
using UniMob;

namespace Samples.SimpleTodoList
{
    public class Todo : ILifetimeScope
    {
        public Todo(Lifetime lifetime, Guid id)
        {
            Lifetime = lifetime;
            Id = id;
        }

        public Lifetime Lifetime { get; }

        public Guid Id { get; }

        [Atom] public string Title { get; set; } = "";
        [Atom] public bool Finished { get; set; } = false;
    }
}