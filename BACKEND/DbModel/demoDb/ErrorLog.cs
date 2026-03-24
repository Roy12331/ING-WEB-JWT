using System;

namespace DbModel.demoDb
{
    public class ErrorLog
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public string? StackTrace { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}