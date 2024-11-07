using Bookify.Domain.Apartments;
using Bookify.Domain.Shared;
using Bookify.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Infrastructure.Configurations
{
    public class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
    {

        public void Configure(EntityTypeBuilder<Apartment> builder)
        {
            var converter = new EnumCollectionJsonValueConverter<Amenity>();
            var comparer = new CollectionValueComparer<Amenity>();

            builder
                .Property(e => e.Amenities)
                .HasConversion(converter)
                .Metadata.SetValueComparer(comparer);

            builder.ToTable("Apartments"); 

            builder.HasKey(apartment => apartment.Id);

            builder.OwnsOne(apartment => apartment.Address);

            builder.Property(apartment => apartment.Name)
                .HasMaxLength(200)
                .HasConversion(name => name.Value, value => new Name(value));

            builder.Property(apartment => apartment.Description)
                .HasMaxLength(400)
                .HasConversion(description => description.Value, value => new Description(value));

            builder.OwnsOne(apartment => apartment.Price, priceBuilder =>
            {
                priceBuilder.Property(money => money.Currency)
                    .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
            });

            builder.OwnsOne(apartment => apartment.CleaningFee, priceBuilder =>
            {
                priceBuilder.Property(money => money.Currency)
                    .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
            });



        }
    }
}
