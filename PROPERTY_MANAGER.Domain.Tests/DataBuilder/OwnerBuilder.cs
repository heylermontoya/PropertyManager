﻿using PROPERTY_MANAGER.Domain.Entities;

namespace PROPERTY_MANAGER.Domain.Tests.DataBuilder
{
    public class OwnerBuilder
    {
        private Guid _idOwner;
        private string _name;
        private string _address;
        private string _photo;
        private DateTime _birthday;
        private IEnumerable<Property> _properties;

        public OwnerBuilder()
        {
            _idOwner = Guid.NewGuid();
            _name = "Default Name";
            _address = "Default Address";
            _photo = "https://defaultphoto.com/default.jpg";
            _birthday = DateTime.Now.AddYears(-30);
            _properties = [];
        }

        public Owner Build()
        {
            return new Owner
            {
                IdOwner = _idOwner,
                Name = _name,
                Address = _address,
                Photo = _photo,
                Birthday = _birthday,
                Properties = _properties
            };
        }

        public OwnerBuilder WithIdOwner(Guid idOwner)
        {
            _idOwner = idOwner;
            return this;
        }

        public OwnerBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public OwnerBuilder WithAddress(string address)
        {
            _address = address;
            return this;
        }

        public OwnerBuilder WithPhoto(string photo)
        {
            _photo = photo;
            return this;
        }

        public OwnerBuilder WithBirthday(DateTime birthday)
        {
            _birthday = birthday;
            return this;
        }

        public OwnerBuilder WithProperties(IEnumerable<Property> properties)
        {
            _properties = properties;
            return this;
        }
    }
}
