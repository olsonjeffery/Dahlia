using System;
using System.Linq;
using Dahlia.Models;
using NHibernate;
using NHibernate.Linq;

namespace Dahlia.Repositories
{
    public interface IVersionRepository
    {
        VersionInfo GetCurrent();
    }

    public class VersionRepository : IVersionRepository
    {
        readonly ISession _session;

        public VersionRepository(ISession session)
        {
            _session = session;
        }

        public VersionInfo GetCurrent()
        {
            var version = _session.Query<VersionInfo>()
                           .ToList()
                           .LastOrDefault();

            return version ?? new VersionInfo();

        }
    }
}