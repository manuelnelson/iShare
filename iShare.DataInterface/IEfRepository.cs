namespace iShare.DataInterface
{
    public interface IEfRepository
    {
        /// <summary>
        /// Get the unit of work in this repository
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

    }
}
