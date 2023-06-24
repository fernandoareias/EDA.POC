using EDA.Core.Commands;

namespace EDA.Post.Cmd.Api.Commands
{ 
    public class EditPostCommand : BaseCommand
    {
        public string Message { get; set; }
    }
}
