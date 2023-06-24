using EDA.Core.Commands;

namespace EDA.Post.Cmd.Api.Commands
{
    public class NewPostCommand : BaseCommand
    {
        public string Author { get; set; }
        public string Message { get; set; }
    }
}
