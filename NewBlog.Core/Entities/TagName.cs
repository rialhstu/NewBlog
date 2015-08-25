using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NewBlog.Core.Entities
{
    public class TagName
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Name: Field is required")]
        [StringLength(500, ErrorMessage = "Name: Length should not exceed 500 characters")]
        public virtual string Name { get; set; }
        [Required(ErrorMessage = "UrlSlug: Field is required")]
    [StringLength(500, ErrorMessage = "UrlSlug: Length should not exceed 500 characters")]
        public virtual string UrlName { get; set; }
        public virtual string Description { get; set; }

        [JsonIgnore]
        public virtual IList<PostName> posts { get; set; }
    }
}
