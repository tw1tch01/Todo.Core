namespace Todo.DomainModels.TodoItems.Enums
{
    public static class FilterTodoItemsBy
    {
        public enum Status
        {
            Pending,
            Completed,
            Cancelled,
            InProgress,
            Overdue
        }

        public enum Importance
        {
            Trivial,
            Minor,
            Major,
            Critical
        }

        public enum Priority
        {
            Lowest,
            Low,
            Medium,
            High,
            Urgent
        }
    }
}