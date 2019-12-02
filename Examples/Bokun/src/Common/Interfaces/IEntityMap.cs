using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.Interfaces
{
	public interface IEntityMap<TEntity> where TEntity : class
	{
		void Configure(EntityTypeBuilder<TEntity> entityBuilder);
	}
}