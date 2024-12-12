using SharpDevMind.Common.Application.Messaging;

namespace SharpDevMind.Modules.Posts.Application.Posts.GetPost;

public sealed record GetPostQuery(Guid PostId) : IQuery<PostResponse>;
