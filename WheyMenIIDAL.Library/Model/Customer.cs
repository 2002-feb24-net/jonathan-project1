using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WheyMenDAL.Library.Model
{
    public partial class Customer
    {
        public Customer()
        {
            Order = new HashSet<Order>();
        }

        [Key]
        [Display(Name="Customer ID")]
        public int Id { get;}

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required")]
        [MaxLength(60,ErrorMessage ="Maximum name length is 60")]
        public string Name { get; set; }

        [Required(ErrorMessage = "E-mail address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [MaxLength(30,ErrorMessage ="Maximum username lenght is 30")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [MinLength(3, ErrorMessage = "Minium password length is 3")]
        [MaxLength(100, ErrorMessage = "Maximum password length is 100")]
        [ScaffoldColumn(false)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Password is required")]
        public string Pwd { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage ="Last name is required")]
        [MaxLength(60,ErrorMessage ="Maximum name length is 60")]
        public string LastName { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
