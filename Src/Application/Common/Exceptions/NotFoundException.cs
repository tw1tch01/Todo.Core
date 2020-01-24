using System;

namespace Todo.Common.Exceptions
{
    public class NotFoundException : NotFoundException<Guid>
    {
        public NotFoundException(string entityName, Guid key)
            : base(entityName, key)
        {
        }
    }

    public class NotFoundException<TKey> : Exception
    {
        public NotFoundException(string entityName, TKey key)
            : base($"{entityName} ({key}) record was not found.")
        {
        }
    }
}