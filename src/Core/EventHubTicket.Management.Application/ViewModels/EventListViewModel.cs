﻿namespace EventHubTicket.Management.Application.ViewModels
{
    public class EventListViewModel
    {
        public Guid EventId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string? ImageUrl { get; set; }
    }
}
