﻿namespace MyEiu.Automapper.ViewModel
{
    public class StaffEiuViewModel
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }       
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? Phone { get; set; }

        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public string? Avatar { get; set; }
    }
}
