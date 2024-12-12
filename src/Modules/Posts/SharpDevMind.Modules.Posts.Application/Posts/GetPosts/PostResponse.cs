﻿namespace SharpDevMind.Modules.Posts.Application.Posts.GetPosts;

public sealed record PostResponse(
    Guid Id,
    Guid CategoryId,
    string Title,
    string Content,
    int Rating,
    DateTime CreatedAtUtc,
    DateTime UpdatedAtUtc
);
