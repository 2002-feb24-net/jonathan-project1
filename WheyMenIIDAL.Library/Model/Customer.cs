using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WheyMen.Domain.Model
{
    /// <summary>
    /// Representation of customer database entity 
    /// </summary>
    public partial class Customer
    {
        public Customer()
        {
            Order = new HashSet<Order>();
        }
        /// <summary>
        /// Customer ID
        /// </summary>
        [Key]
        [Display(Name="Customer ID")]
        public int Id { get;}

        /// <summary>
        /// Customer's first name
        /// </summary>
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required")]
        [MaxLength(60,ErrorMessage ="Maximum name length is 60")]
        public string Name { get; set; }

        /// <summary>
        /// Customer's email
        /// </summary>
        [Required(ErrorMessage = "E-mail address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        /// <summary>
        /// Customer's user name, has sa maximum length of 30
        /// </summary>
        [Required(ErrorMessage = "Username is required")]
        [MaxLength(30,ErrorMessage ="Maximum username lenght is 30")]
        public string Username { get; set; }
    
        /// <summary>
        /// Password, between 3 and 100 characters
        /// </summary>
        [Display(Name = "Password")]
        [MinLength(3, ErrorMessage = "Minium password length is 3")]
        [MaxLength(100, ErrorMessage = "Maximum password length is 100")]
        [ScaffoldColumn(false)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Password is required")]
        public string Pwd { get; set; }

        /// <summary>
        /// Customer's last name
        /// </summary>
        [Display(Name = "Last Name")]
        [Required(ErrorMessage ="Last name is required")]
        [MaxLength(60,ErrorMessage ="Maximum name length is 60")]
        public string LastName { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
