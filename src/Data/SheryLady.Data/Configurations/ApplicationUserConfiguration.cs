﻿namespace SheryLady.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;
    using static Common.ModelConstants;

    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> user)
        {
            user
                .Property(u => u.FirstName)
                .HasMaxLength(UserFirstNameMaxLength)
                .IsRequired();

            user
                .Property(u => u.LastName)
                .HasMaxLength(UserLastNameMaxLength)
                .IsRequired();

            user
                .Property(u => u.ProfilePicture)
                .HasMaxLength(UserProfilePictureMaxLength);

            user
                .HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            user
                .HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            user
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            user
                .HasIndex(u => u.IsDeleted);
        }
    }
}