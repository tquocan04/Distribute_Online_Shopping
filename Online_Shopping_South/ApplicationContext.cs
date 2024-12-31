using Microsoft.EntityFrameworkCore;
using Online_Shopping_South.Entities;

namespace Online_Shopping_South
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<BranchProduct> BranchProducts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ShippingMethod> ShippingMethods { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(c => c.Name).IsUnique();
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(c => c.Email).IsUnique();

                entity.HasMany(c => c.Addresses)
                    .WithOne(a => a.Customer)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.HasIndex(c => c.Name).IsUnique();

                entity.HasMany(b => b.Employees)
                    .WithOne(e => e.Branch)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasMany(b => b.Addresses)
                    .WithOne(a => a.Branch)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(p => p.Name).IsUnique();
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(i => new { i.ProductId, i.OrderId });
            });

            modelBuilder.Entity<BranchProduct>(entity =>
            {
                entity.HasKey(bp => new { bp.ProductId, bp.BranchId });
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasIndex(c => c.Email).IsUnique();
                entity.HasIndex(c => c.Username).IsUnique();

                entity.HasMany(e => e.Addresses)
                    .WithOne(a => a.Employee)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Voucher>(entity =>
            {
                entity.HasIndex(v => v.Code).IsUnique();

                entity.HasMany(v => v.Orders)
                    .WithOne(o => o.Voucher)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer) // Thiết lập quan hệ 1-n
                .WithMany(c => c.Orders) // Customer có nhiều Orders
                .OnDelete(DeleteBehavior.Cascade); // Xóa cascade

            modelBuilder.Entity<Region>(entity =>
            {
                entity.HasIndex(c => c.Name).IsUnique();
                entity.HasData(
                    new Region { Id = "Bac", Name = "Miền Bắc" },
                    new Region { Id = "Trung", Name = "Miền Trung" },
                    new Region { Id = "Nam", Name = "Miền Nam" }
                );
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasIndex(c => c.Name).IsUnique();
                entity.HasData(
                    //miền bắc
                    new City
                    {
                        Id = Guid.Parse("1b84594b-aa7f-4a69-b54a-96cbf9b17c6e"),
                        Name = "Hà Nội",
                        RegionId = "Bac"
                    },
                    new City
                    {
                        Id = Guid.Parse("0bdea930-da3d-40c6-97cd-b3969f8014c7"),
                        Name = "Hải Phòng",
                        RegionId = "Bac"
                    },
                    new City
                    {
                        Id = Guid.Parse("4a486645-052b-4a56-bb36-75c7e876ae2d"),
                        Name = "Hải Dương",
                        RegionId = "Bac"
                    },
                    new City
                    {
                        Id = Guid.Parse("2df88e32-3919-494d-b489-dbf4258fc245"),
                        Name = "Phú Thọ",
                        RegionId = "Bac"
                    },
                    new City
                    {
                        Id = Guid.Parse("14b0bd4d-27af-496e-aa9c-3e1d532f5038"),
                        Name = "Quảng Ninh",
                        RegionId = "Bac"
                    },

                    //miền trung
                    new City
                    {
                        Id = Guid.Parse("fc446281-359c-46ec-a2b9-bf9f26014f88"),
                        Name = "Đà Nẵng",
                        RegionId = "Trung"
                    },
                    new City
                    {
                        Id = Guid.Parse("3321ed88-441b-4121-9ead-e154544185e1"),
                        Name = "Thừa-Thiên Huế",
                        RegionId = "Trung"
                    },
                    new City
                    {
                        Id = Guid.Parse("aec5e588-017c-4da1-91e8-b8bc1888056e"),
                        Name = "Quảng Nam",
                        RegionId = "Trung"
                    },
                    new City
                    {
                        Id = Guid.Parse("a41ba56a-b53b-42f6-8c56-04dcbbde7905"),
                        Name = "Quảng Trị",
                        RegionId = "Trung"
                    },

                    //miền nam
                    new City
                    {
                        Id = Guid.Parse("b40f6c23-15f7-460c-8f94-fdcbe33cda68"),
                        Name = "Bình Dương",
                        RegionId = "Nam"
                    },
                    new City
                    {
                        Id = Guid.Parse("bbf96ba4-7836-4c53-af1a-e3e572f31ebf"),
                        Name = "Bà Rịa Vũng Tàu",
                        RegionId = "Nam"
                    },
                    new City
                    {
                        Id = Guid.Parse("acd51ba8-d6e3-4110-831e-5147f8fe2c96"),
                        Name = "Đồng Nai",
                        RegionId = "Nam"
                    },
                    new City
                    {
                        Id = Guid.Parse("6f624665-053e-45d2-8dd6-42baa124b481"),
                        Name = "Hồ Chí Minh",
                        RegionId = "Nam"
                    },
                    new City
                    {
                        Id = Guid.Parse("ad453439-a309-42a3-917c-d6aaa67ac9ca"),
                        Name = "Long An",
                        RegionId = "Nam"
                    }
                );
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.HasIndex(d => d.Name).IsUnique();
                entity.HasData(
                    //Quảng Ninh
                    new District
                    {
                        Id = Guid.Parse("289f3f8b-7b77-491e-9892-043cee73f0a3"),
                        CityId = Guid.Parse("14b0bd4d-27af-496e-aa9c-3e1d532f5038"),
                        Name = "Cẩm Phả"
                    },
                    new District
                    {
                        Id = Guid.Parse("412da1ea-5f1b-4aa3-9c13-cf5d557b59e3"),
                        CityId = Guid.Parse("14b0bd4d-27af-496e-aa9c-3e1d532f5038"),
                        Name = "Móng Cái"
                    },
                    new District
                    {
                        Id = Guid.Parse("1bc7754e-bacc-484f-b5cc-7e2df41f1f30"),
                        CityId = Guid.Parse("14b0bd4d-27af-496e-aa9c-3e1d532f5038"),
                        Name = "Hạ Long"
                    },
                    new District
                    {
                        Id = Guid.Parse("1be59787-ee81-4824-b7e1-766e71fffa6b"),
                        CityId = Guid.Parse("14b0bd4d-27af-496e-aa9c-3e1d532f5038"),
                        Name = "Uông Bí"
                    },
                    //Hải Dương
                    new District
                    {
                        Id = Guid.Parse("4af1f7fa-cd35-4940-b2f9-8811ca9a2b75"),
                        CityId = Guid.Parse("4a486645-052b-4a56-bb36-75c7e876ae2d"),
                        Name = "Chí Linh"
                    },
                    new District
                    {
                        Id = Guid.Parse("2f657b93-1d8b-4024-bfd9-827009d98c67"),
                        CityId = Guid.Parse("4a486645-052b-4a56-bb36-75c7e876ae2d"),
                        Name = "Hải Dương"
                    },
                    new District
                    {
                        Id = Guid.Parse("1f461ef4-7dbc-4f7c-b51e-a16a5cfca7d3"),
                        CityId = Guid.Parse("4a486645-052b-4a56-bb36-75c7e876ae2d"),
                        Name = "Gia Lộc"
                    },
                    //Phú Thọ
                    new District
                    {
                        Id = Guid.Parse("eef65a95-e294-46a3-828f-5e44ca4b2c77"),
                        CityId = Guid.Parse("2df88e32-3919-494d-b489-dbf4258fc245"),
                        Name = "Hạ Hòa"
                    },
                    new District
                    {
                        Id = Guid.Parse("91d1c4dd-f364-412d-a7a2-8857f4b8a9c9"),
                        CityId = Guid.Parse("2df88e32-3919-494d-b489-dbf4258fc245"),
                        Name = "Thanh Thủy"
                    },
                    new District
                    {
                        Id = Guid.Parse("db4e7867-1156-44d9-825b-8f42ae9712fe"),
                        CityId = Guid.Parse("2df88e32-3919-494d-b489-dbf4258fc245"),
                        Name = "Yên Lập"
                    },
                    //Hải Phòng
                    new District
                    {
                        Id = Guid.Parse("9744402f-34c1-4555-baff-9450ab73303a"),
                        CityId = Guid.Parse("0bdea930-da3d-40c6-97cd-b3969f8014c7"),
                        Name = "Hồng Bàng"
                    },
                    new District
                    {
                        Id = Guid.Parse("033fbf94-3a48-4bec-a1d1-13f0dcca7ff2"),
                        CityId = Guid.Parse("0bdea930-da3d-40c6-97cd-b3969f8014c7"),
                        Name = "Lê Chân"
                    },
                    new District
                    {
                        Id = Guid.Parse("aabecff8-16d3-4298-839b-c5ec84ae49a3"),
                        CityId = Guid.Parse("0bdea930-da3d-40c6-97cd-b3969f8014c7"),
                        Name = "Kiến An"
                    },
                    //Hà Nội
                    new District
                    {
                        Id = Guid.Parse("bfb4be74-f5ed-4e67-94ba-7ee067a2098d"),
                        CityId = Guid.Parse("1b84594b-aa7f-4a69-b54a-96cbf9b17c6e"),
                        Name = "Ba Đình"
                    },
                    new District
                    {
                        Id = Guid.Parse("b999d3eb-a753-49f4-897f-4c37002e1302"),
                        CityId = Guid.Parse("1b84594b-aa7f-4a69-b54a-96cbf9b17c6e"),
                        Name = "Cầu Giấy"
                    },
                    new District
                    {
                        Id = Guid.Parse("b051a4a6-66ba-4220-95e4-59abd37d4e0b"),
                        CityId = Guid.Parse("1b84594b-aa7f-4a69-b54a-96cbf9b17c6e"),
                        Name = "Đống Đa"
                    },
                    new District
                    {
                        Id = Guid.Parse("83ba4e24-9a0f-4c6a-b822-29a1eb5f4d3f"),
                        CityId = Guid.Parse("1b84594b-aa7f-4a69-b54a-96cbf9b17c6e"),
                        Name = "Hoàn Kiếm"
                    },
                    //Huế
                    new District
                    {
                        Id = Guid.Parse("a9e0ff46-2f5c-463a-a793-08e9a533900c"),
                        CityId = Guid.Parse("3321ed88-441b-4121-9ead-e154544185e1"),
                        Name = "Hương Thủy"
                    },
                    new District
                    {
                        Id = Guid.Parse("4b1dd408-19c8-40cc-a39a-02ecaf53cfbe"),
                        CityId = Guid.Parse("3321ed88-441b-4121-9ead-e154544185e1"),
                        Name = "Phong Điền"
                    },
                    new District
                    {
                        Id = Guid.Parse("5e9c531c-cf58-4f0b-a447-d1a1cfd2b1b6"),
                        CityId = Guid.Parse("3321ed88-441b-4121-9ead-e154544185e1"),
                        Name = "Phú Vang"
                    },
                    //Đà Nẵng
                    new District
                    {
                        Id = Guid.Parse("2e8e7f13-ca6a-42c7-a1a0-fc5b4c872b3f"),
                        CityId = Guid.Parse("fc446281-359c-46ec-a2b9-bf9f26014f88"),
                        Name = "Hải Châu"
                    },
                    new District
                    {
                        Id = Guid.Parse("9fc84eab-708a-49a2-b819-06d0629e560a"),
                        CityId = Guid.Parse("fc446281-359c-46ec-a2b9-bf9f26014f88"),
                        Name = "Hoàng Sa"
                    },
                    //Quảng Nam
                    new District
                    {
                        Id = Guid.Parse("24de8ac0-247f-40e4-b472-1171b56c1e74"),
                        CityId = Guid.Parse("aec5e588-017c-4da1-91e8-b8bc1888056e"),
                        Name = "Hội An"
                    },
                    new District
                    {
                        Id = Guid.Parse("8c132e18-9710-402f-a5c0-7ad8dd90311b"),
                        CityId = Guid.Parse("aec5e588-017c-4da1-91e8-b8bc1888056e"),
                        Name = "Tam Kỳ"
                    },
                    //Quảng Trị
                    new District
                    {
                        Id = Guid.Parse("603cf681-062c-4378-89d0-a534ab196661"),
                        CityId = Guid.Parse("a41ba56a-b53b-42f6-8c56-04dcbbde7905"),
                        Name = "Cam Lộ"
                    },
                    new District
                    {
                        Id = Guid.Parse("aab01227-c628-4ad0-a7c3-69ce33712109"),
                        CityId = Guid.Parse("a41ba56a-b53b-42f6-8c56-04dcbbde7905"),
                        Name = "Hải Lăng"
                    },
                    //HCM
                    new District
                    {
                        Id = Guid.Parse("53b0b29e-7b2c-4d4b-accc-f693ce746539"),
                        CityId = Guid.Parse("6f624665-053e-45d2-8dd6-42baa124b481"),
                        Name = "Quận 1"
                    },
                    new District
                    {
                        Id = Guid.Parse("c893d9cf-7b2d-4ebc-9c65-3f78e0fce6bb"),
                        CityId = Guid.Parse("6f624665-053e-45d2-8dd6-42baa124b481"),
                        Name = "Quận 5"
                    },
                    new District
                    {
                        Id = Guid.Parse("f9be5b5e-847d-47e9-847a-0150c4f608e1"),
                        CityId = Guid.Parse("6f624665-053e-45d2-8dd6-42baa124b481"),
                        Name = "Quận 10"
                    },
                    //Đông Nai
                    new District
                    {
                        Id = Guid.Parse("93811fdc-4e5e-4e92-87dd-91650cfe357a"),
                        CityId = Guid.Parse("acd51ba8-d6e3-4110-831e-5147f8fe2c96"),
                        Name = "Biên Hòa"
                    },
                    new District
                    {
                        Id = Guid.Parse("05e87ab6-1238-412d-9d93-88902310ee89"),
                        CityId = Guid.Parse("acd51ba8-d6e3-4110-831e-5147f8fe2c96"),
                        Name = "Trảng Bom"
                    },
                    new District
                    {
                        Id = Guid.Parse("e60650de-7772-49d5-ac72-81d3bfa774d4"),
                        CityId = Guid.Parse("acd51ba8-d6e3-4110-831e-5147f8fe2c96"),
                        Name = "Long Khánh"
                    },
                    new District
                    {
                        Id = Guid.Parse("90c585ba-5d16-4406-ad97-41c34732ccd3"),
                        CityId = Guid.Parse("acd51ba8-d6e3-4110-831e-5147f8fe2c96"),
                        Name = "Long Thành"
                    },
                    //Vũng tàu
                    new District
                    {
                        Id = Guid.Parse("283942d0-07a6-44c4-a8e3-af3372c4f4d7"),
                        CityId = Guid.Parse("bbf96ba4-7836-4c53-af1a-e3e572f31ebf"),
                        Name = "Thị xã Bà Rịa"
                    },
                    new District
                    {
                        Id = Guid.Parse("ab041f51-dcf0-4071-b1ae-6ebeaf3c4840"),
                        CityId = Guid.Parse("bbf96ba4-7836-4c53-af1a-e3e572f31ebf"),
                        Name = "Châu Đốc"
                    },
                    new District
                    {
                        Id = Guid.Parse("f6220d40-db0b-4be5-ada4-5996bab22cd0"),
                        CityId = Guid.Parse("bbf96ba4-7836-4c53-af1a-e3e572f31ebf"),
                        Name = "Xuyên Mộc"
                    },
                    //Bình Dương
                    new District
                    {
                        Id = Guid.Parse("c56546a3-e495-4662-a7f4-9196eccbdbf7"),
                        CityId = Guid.Parse("b40f6c23-15f7-460c-8f94-fdcbe33cda68"),
                        Name = "Dĩ An"
                    },
                    new District
                    {
                        Id = Guid.Parse("8f23aec1-ad93-4170-8493-b388da9ec33e"),
                        CityId = Guid.Parse("b40f6c23-15f7-460c-8f94-fdcbe33cda68"),
                        Name = "Tân Uyên"
                    },
                    new District
                    {
                        Id = Guid.Parse("af5c6bad-cba1-459c-aacc-e31438f4ba31"),
                        CityId = Guid.Parse("b40f6c23-15f7-460c-8f94-fdcbe33cda68"),
                        Name = "Thủ Dầu Một"
                    },
                    //Long An
                    new District
                    {
                        Id = Guid.Parse("57fa0862-5fc6-4e0b-bc55-b8dda3860fc8"),
                        CityId = Guid.Parse("ad453439-a309-42a3-917c-d6aaa67ac9ca"),
                        Name = "Đức Huệ"
                    },
                    new District
                    {
                        Id = Guid.Parse("9d442ba3-12d2-4845-9a9c-92d88979bd96"),
                        CityId = Guid.Parse("ad453439-a309-42a3-917c-d6aaa67ac9ca"),
                        Name = "Tân An"
                    }
                );
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasData(
                    new Payment
                    {
                        Id = "MOMO",
                        Name = "MOMO",
                        Image = null
                    },
                    new Payment
                    {
                        Id = "ZALOPAY",
                        Name = "ZALOPAY",
                        Image = null
                    },
                    new Payment
                    {
                        Id = "CASH",
                        Name = "CASH",
                        Image = null
                    });
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasData(
                    new Role
                    {
                        Id = "Staff",
                        Name = "STAFF",
                    },
                    new Role
                    {
                        Id = "Admin",
                        Name = "ADMIN",
                    });
            });

            modelBuilder.Entity<ShippingMethod>(entity =>
            {
                entity.HasData(
                    new ShippingMethod
                    {
                        Id = "GRAB",
                        Name = "GRAB",
                    },
                    new ShippingMethod
                    {
                        Id = "BE",
                        Name = "BE",
                    },
                    new ShippingMethod
                    {
                        Id = "SHOPEEFOOD",
                        Name = "SHOPEE FOOD",
                    });
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasData(
                    new Employee
                    {
                        Id = Guid.Parse("7c30c0cd-f8df-48b4-9eba-8c56c7c185c8"),
                        Name = "Đỗ Trần Tuấn Anh",
                        Gender = "Nam",
                        Username = "adminS",
                        Email = "adminS@gmail.com",
                        Password = "tuananh",
                        PhoneNumber = "0256478983",
                        Dob = new DateOnly(2004, 1, 1),
                        RoleId = "Admin",
                        BranchId = null,
                    });
            });

            modelBuilder.Entity<Address>(entity =>
            {
                //Admin Nam
                entity.HasData(
                    new Address
                    {
                        Id = Guid.Parse("a4e84922-0b61-4031-93ba-b074e85f7197"),
                        EmployeeId = Guid.Parse("7c30c0cd-f8df-48b4-9eba-8c56c7c185c8"),
                        DistrictId = Guid.Parse("53b0b29e-7b2c-4d4b-accc-f693ce746539"),
                        Street = "Miền Nam, HCM, Q1",
                        IsDefault = true,
                    });
            });
        }
    }
}
