//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjektBazyDanych
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class User
    {
        [Display(Name = "Login")]
        public string login { get; set; }
        [Display(Name = "Has�o zaszyfrowane")]
        public string passwordHash { get; set; }
        [Display(Name = "Has�o")]
        public string passwordSalt { get; set; }
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "Imi�")]
        [Required(ErrorMessage = "Wpisz imi�")]
        [RegularExpression("[^0-9]*", ErrorMessage = "Imi� nie mo�e zawiera� cyfr")]
        public string firstName { get; set; }
        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "Wpisz nazwisko")]
        [RegularExpression("[^0-9]*", ErrorMessage = "Nazwisko nie mo�e zawiera� cyfr")]
        public string lastName { get; set; }
    }
}
