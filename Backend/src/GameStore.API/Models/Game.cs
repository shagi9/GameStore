﻿using System.ComponentModel.DataAnnotations;

namespace GameStore.API.Models
{
    public class Game
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

        public required Genre Genre { get; set; }

        [Range(1, 100)]
        public decimal Price { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public required string Description { get; set; }
    }
}
