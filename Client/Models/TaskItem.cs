using System;
using System.Collections.Generic;

namespace Client.Models
{
    public partial class TaskItem
    {
        public int Id { get; set; }
        public string Module { get; set; }
        public string Task { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public double Efforts { get; set; }
        public string Issue { get; set; }
    }
}
