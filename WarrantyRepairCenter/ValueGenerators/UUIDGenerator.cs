using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace WarrantyRepairCenter.ValueGenerators
{
    internal class UUIDGenerator : ValueGenerator<Guid>
    {
        public override bool GeneratesTemporaryValues => false;

        public override Guid Next(EntityEntry entry) => UUIDNext.Uuid.NewDatabaseFriendly(UUIDNext.Database.SqlServer);
    }
}