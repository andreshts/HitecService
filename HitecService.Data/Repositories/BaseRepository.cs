using HitecService.Data.Database;

namespace HitecService.Data.Repositories;

public class BaseRepository
{
    protected readonly HitecServiceDbContext Context;

    protected BaseRepository(HitecServiceDbContext context)
        => Context = context;
}