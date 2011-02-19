using System;
using System.Collections.Generic;

namespace Dahlia.ViewModels
{
    public class RetreatListViewModel
    {
        public IEnumerable<RetreatListRetreatViewModel> Retreats { get; set; }
    }

    public class RetreatListRetreatViewModel
    {
        public DateTime Date { get; set; }
        public Uri AddParticipantLink { get; set; }
    }
}