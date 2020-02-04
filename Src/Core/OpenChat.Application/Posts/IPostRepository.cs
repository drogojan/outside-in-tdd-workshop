using OpenChat.Domain.Entities;

namespace OpenChat.Application.Posts
{
    public interface IPostRepository
    {
        void Add(Post post);
    }
}