using CleanArchitecture.BuildingBlocks.EventBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.IntegrationEvents
{
    public class SampleAutomationEvent : IntegrationEvent
    {
        public Guid Id { get; set; }
        public string InitiatedBy { get; set; }

        public SampleAutomationEvent(Guid id, string initiatedBy )
        {
            Id = id;
            InitiatedBy = initiatedBy;
        }

    }
}
