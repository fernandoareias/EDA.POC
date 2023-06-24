using EDA.Core.Commands;

namespace EDA.Post.Cmd.Api.Commands
{
    public class DeletePostCommand : BaseCommand
    {
        public string Username { get; set; }
    }
}
