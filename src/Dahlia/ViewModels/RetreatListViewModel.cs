using System;
using System.Collections.Generic;
using Dahlia.Models;
using Dahlia.Services;

namespace Dahlia.ViewModels
{
    public class RetreatListViewModel
    {
        public IEnumerable<RetreatListRetreatViewModel> Retreats { get; set; }
        public Uri CreateLink { get; set; }
        public int CurrentRetreatId { get; set; }
    }

    public class RetreatListRetreatViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }

        public Uri AddParticipantLink { get; set; }
        public IEnumerable<RetreatListParticipantViewModel> RegisteredParticipants { get; set; }
    }

    public class RetreatListParticipantViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BedCode { get; set; }
        public DateTime DateReceived { get; set; }
        public string Notes { get; set; }
        public PhysicalStatus PhysicalStatus { get; set; }
        public Uri DeleteLink { get; set; }
    }
}
