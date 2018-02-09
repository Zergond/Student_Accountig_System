using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class StudentTableViewModel
    {
      
        [Required]
        [MaxLength(20, ErrorMessage = "Name name can't be longer than 20 characters")]
        public string Name { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Last name can't be longer than 20 characters")]
        public string LastName { get; set; }
        [Required]
        [Range(0, 120, ErrorMessage = "Age has to be between 0 and 120")]
        public int Age { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime StudyDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime RegisteredDate { get; set; }
    }
}