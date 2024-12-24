using CleanArchitecture.BuildingBlocks.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.IntegrationEvents
{
    public class SampleAutomation1Event : IntegrationEvent
    {
        public Guid Id { get; set; }
        public string InitiatedBy { get; set; }

        public SampleAutomation1Event(Guid id, string initiatedBy)
        {
            Id = id;
            InitiatedBy = initiatedBy;
        }

    }
}
