﻿using System;

namespace CostTracking.Domain
{
    public class TimeEntry
    {
        public DateTime DateWorked { get; private set; }
        public decimal HoursWorked { get; private set; }

        public TimeEntry(DateTime dateWorked, decimal hoursWorked)
        {
            DateWorked = dateWorked;
            HoursWorked = hoursWorked;
        }
    }
}