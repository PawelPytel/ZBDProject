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

    public partial class ServicePointWorker
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ServicePointWorker()
        {
            this.ServicePoints = new HashSet<ServicePoint>();
        }

        [Display(Name = "Id pracownika")]
        public int id { get; set; }
        [Display(Name = "Imi�")]
        [Required(ErrorMessage = "Wpisz imi�")]
        [RegularExpression("[^0-9]*", ErrorMessage = "Imi� nie mo�e zawiera� cyfr")]
        public string firstName { get; set; }
        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "Wpisz nazwisko")]
        [RegularExpression("[^0-9]*", ErrorMessage = "Nazwisko nie mo�e zawiera� cyfr")]
        public string lastName { get; set; }
        [Display(Name = "Wiek")]
        [Required(ErrorMessage = "Wpisz wiek")]
        [Range(13, 99, ErrorMessage = "Prosz� poda� odpowiedni wiek")]
        public int age { get; set; }
        [Display(Name = "Pensja w z�")]
        [Required(ErrorMessage = "Wpisz pensj�")]
        [RegularExpression("[0-9]*", ErrorMessage = "Prosz� poda� liczb� dodatni�")]
        public int salary { get; set; }
        [Display(Name = "Zatrudniony")]
        [Required(ErrorMessage = "Wybierz dat�")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime employed { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServicePoint> ServicePoints { get; set; }
    }
}
