using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindingNemo.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class MemoryViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Note { get; set; }

        [Required]
        public int SelectedTagId { get; set; }

        [Display(Name = "Tags")]
        public IEnumerable<SelectListItem> Tags { get; set; }
    }
}