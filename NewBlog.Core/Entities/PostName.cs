using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NewBlog.Core.Entities
{
    public class PostName
    {
        [Required(ErrorMessage = "Id: Field is required")]
        public virtual int Id { get; set; }
        
        [Required(ErrorMessage = "Title: Field is required")]
        [StringLength(500, ErrorMessage = "Title: Length should not exceed 500 characters")]
        public virtual string TitleName { get; set; }
      
        [Required(ErrorMessage = "ShortDescription: Field is required")]
        public virtual string ShortDescription
        { get; set; }
        [Required(ErrorMessage = "Description: Field is required")]
        public virtual string Description { get; set; }

        [Required(ErrorMessage = "Meta: Field is required")]
        [StringLength(1000, ErrorMessage = "Meta: Length should not exceed 1000 characters")]
        public virtual string Meta { get; set; }
      
        [Required(ErrorMessage = "Meta: Field is required")]
        [StringLength(1000, ErrorMessage = "Meta: UrlSlug should not exceed 50 characters")]
       
        public virtual string UrlName { get; set; }

        public virtual bool Published { get; set; }
     [Required(ErrorMessage = "PostedOn: Field is required")]
        public virtual DateTime PostedDate { get; set; }

        public virtual DateTime? Modified { get; set; }

        public virtual CategoryName cagegory { get; set; }

        public virtual IList<TagName> tags { get; set; }

    }
}
