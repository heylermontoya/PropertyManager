﻿using MediatR;
using PROPERTY_MANAGER.Application.DTOs;
using PROPERTY_MANAGER.Domain.Entities;
using PROPERTY_MANAGER.Domain.Services.property;

namespace PROPERTY_MANAGER.Application.Feature.property.Commands
{
    public class CreatePropertyCommandHandler(
        PropertyService service
    ) : IRequestHandler<CreatePropertyCommand, PropertyDto>
    {
        public async Task<PropertyDto> Handle(
            CreatePropertyCommand command,
            CancellationToken cancellationToken
        )
        {
            Property property = await service.CreatePropertyAsync(
                command.Name,
                command.Address,
                command.Price,
                command.CodeInternal,
                command.Year,
                command.IdOwner
            );

            return MapPropertyToPropertyDto(property);
        }

        private static PropertyDto MapPropertyToPropertyDto(Property property)
        {
            return new PropertyDto()
            {
                IdProperty = property.IdProperty,
                NameProperty = property.Name,
                Address = property.Address,
                Price = property.Price,
                CodeInternal = property.CodeInternal,
                Year = property.Year,
                IdOwner = property.IdOwner
            };
        }
    }
}
