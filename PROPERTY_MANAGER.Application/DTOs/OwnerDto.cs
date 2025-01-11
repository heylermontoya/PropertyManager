﻿namespace PROPERTY_MANAGER.Application.DTOs
{
    public class OwnerDto
    {
        public Guid IdOwner { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
    }
}
