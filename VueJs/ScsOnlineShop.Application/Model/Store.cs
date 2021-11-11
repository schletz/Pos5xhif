using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScsOnlineShop.Application.Model
{
    public class Store
    {
        public Store(string name)
        {
            Name = name;
            Guid = Guid.NewGuid();
        }

        protected Store()
        {
        }

        public int Id { get; private set; }

        public string Name { get; set; } = default!;
        public virtual ICollection<Offer> Offers { get; } = new List<Offer>();

        // Muss mit HasAlternateKey() in OnModelCreating definiert werden.
        public Guid Guid { get; }
    }
}