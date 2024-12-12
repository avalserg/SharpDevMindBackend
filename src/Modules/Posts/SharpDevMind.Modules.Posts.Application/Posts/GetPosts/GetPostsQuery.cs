using SharpDevMind.Common.Application.Messaging;

namespace SharpDevMind.Modules.Posts.Application.Posts.GetPosts;

public sealed record GetPostsQuery : IQuery<IReadOnlyCollection<PostResponse>>;
