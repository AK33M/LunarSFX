﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LunarSFX.Core.Objects
{
    public class Tag
    {
        public virtual int Id { get; set; }
        [Required(ErrorMessage = "Name: Field is required")]
        [StringLength(500, ErrorMessage = "Name: Length should not exceed 500 characters")]
        public virtual string Name { get; set; }
        [Required(ErrorMessage = "UrlSlug: Field is required")]
        [StringLength(500, ErrorMessage = "UrlSlug: Length should not exceed 500 characters")]
        public virtual string UrlSlug { get; set; }
        public virtual string Description { get; set; }
        [JsonIgnore]
        public virtual IList<Post> Posts { get; set; }
    }
}