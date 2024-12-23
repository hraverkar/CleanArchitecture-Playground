using CleanArchitecture.BuildingBlocks.EventBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.IntegrationEvents
{
    public class TaxAutomationEvent : IntegrationEvent
    {
        public Guid Id { get; set; }
        public string InitiatedBy { get; set; }

        public TaxAutomationEvent(Guid id, string initiatedBy )
        {
            Id = id;
            InitiatedBy = initiatedBy;
        }

    }
}
