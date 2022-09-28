namespace MediatRExample.Common.Commands
{
    public class CommandBase : ICommand
    {
        public CommandBase()
        {
            this.Id = Guid.NewGuid();
        }

        protected CommandBase(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }
}
