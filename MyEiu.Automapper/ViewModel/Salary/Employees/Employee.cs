using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Automapper.ViewModel.Salary.Employees
{
    public class Employee
    {
        public string Id { get; set; }
        public string StaffId { get; set; }
        public int? OrderNumber { get; set; }
        public string FamilyName { get; set; }
        public string GivenName { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfHire { get; set; }
        public DateTime? BirthDay { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CitizenIdNumber { get; set; }
        public string CitizenIdIssuedPlace { get; set; }
        public DateTime? CitizenIdIssuedDate { get; set; }
        public DateTime? CitizenIdExpiredDate { get; set; }
        public string Nationality { get; set; }
        public string Ethnic { get; set; }
        public string Religion { get; set; }
        public string WorkingStatus { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
    }
}
