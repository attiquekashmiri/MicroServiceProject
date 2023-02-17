using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCommon.Models
{
    public class Employee
    {
        [Key]
        public long Id { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName  { get; set; }
        public DateTime DateOfBirth  { get; set; }
        public DateTime JoiningDate  { get; set; }
        public DateTime AddedDateTime  { get; set; }
        public DateTime UpdatedDateTime  { get; set; }
        public string City  { get; set; }
        public string Country  { get; set; }
        public string Street  { get; set; }
        public long? ZipCode  { get; set; }
        public double? Salary  { get; set; }
        public string PositionTitle  { get; set; }
        public bool isActive  { get; set; }
    }
}
