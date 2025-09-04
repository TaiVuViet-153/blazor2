using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EnrollCourses.Models
{
    public class UserVM
    {
        [Required(ErrorMessage = "Full Name is required")]
        [MinLength(2, ErrorMessage = "Full Name is at least 3 characters")]
        [MaxLength(30, ErrorMessage = "Full Name is maximize 30 characters")]
        [RegularExpression(@"^[A-Z][a-z]+(?:\s[A-Z][a-z]+)*$", ErrorMessage = "Full Name is invalid. Please capitalize the first letter of each word. Special characters and numbers are not allowed")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [MinLength(6, ErrorMessage = "Email is at least 6 characters")]
        [MaxLength(256, ErrorMessage = "Email is maximize 256 characters")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]{1,64}@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email is invalid format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression("^0[3|5|7|8|9][0-9]{8}$", ErrorMessage = "Phone Number is invalid Vietnamese Format")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage= "Please select your course")]
        public string SelectedCourse { get; set; }

        [Required(ErrorMessage= "Learning Method is required")]
        public bool LearningMethod { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        // [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Today;

        public string Note { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = "Please accept the terms and conditions before submit")]
        public bool IsAcceptTerms { get; set; }
    }
}