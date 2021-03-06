﻿namespace FreelanceMe.Web.Areas.Administration.Models.InputModels
{
    using FreelanceMe.Data.Models;
    using FreelanceMe.Web.Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class CategoryInputModel : IMapFrom<Category>
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        [UIHint("Description")]
        public string Description { get; set; }
    }
}