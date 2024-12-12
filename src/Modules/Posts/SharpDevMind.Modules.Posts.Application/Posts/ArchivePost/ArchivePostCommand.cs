using SharpDevMind.Common.Application.Messaging;

namespace SharpDevMind.Modules.Posts.Application.Posts.ArchivePost;

public sealed record ArchivePostCommand(Guid PostId) : ICommand;
