﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagerMVC.Models
{
    [Table("Employees")]
    public class Employee
    {
        [Column("EmployeeID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Employee ID is Required")]
        [Display(Name = "Employee ID")]
        public int EmployeeID { get; set; }

        [Column("FirstName")]
        [Display(Name = "First Name")]
        [StringLength(20, ErrorMessage = ("First Name must be less than 20 characters"))]
        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }


        [Column("LastName")]
        [Display(Name = "Last Name")]
        [StringLength(20, ErrorMessage = ("Last Name must be less than 20 characters"))]
        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }

        [Column("Title")]
        [Display(Name = "Title")]
        [StringLength(30, ErrorMessage = ("Last Name must be less than 30 characters"))]
        [Required(ErrorMessage = "Title is Required")]
        public string Title { get; set; }

        [Column("BirthDate")]
        [Display(Name = "Birth Date")]
        [Required(ErrorMessage = "Birth Date is Required")]
        public DateTime BirthDate { get; set; }

        [Column("HireDate")]
        [Display(Name = "Hire Date")]

        [Required(ErrorMessage = "Hire Date is Required")]
        public DateTime HireDate { get; set; }

        [Column("Country")]
        [Display(Name = "Country")]
        [StringLength(30, ErrorMessage = ("Country must be less than 30 characters"))]
        [Required(ErrorMessage = "Country is Required")]
        public string Country { get; set; }

        [Column("Notes")]
        [Display(Name = "Notes")]
        [StringLength(500, ErrorMessage = ("Notes must be less than 500 characters"))]
        public string Notes { get; set; }
    }
}
