﻿using System;
using System.Collections.Generic;

namespace SPG_Fachtheorie.Aufgabe2.Model
{
    public class Show
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        protected Show()
        { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Show(Event @event, DateTime date)
        {
            Event = @event;
            Date = date;
        }

        public int Id { get; set; }
        public Event Event { get; set; }
        public DateTime Date { get; set; }
        public List<Contingent> Contingents { get; } = new();
    }
}