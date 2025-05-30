﻿using System.ComponentModel.DataAnnotations;

namespace BlazorBookHub.Shared.Models
{
    public class UserProfileDto
    {
        public string Id { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string? AvatarUrl { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        public string? Gender { get; set; }
    }
}
