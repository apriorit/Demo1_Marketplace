namespace Marketplace.Contracts
{
    /// <summary>
    /// Represents class for entities' conversions.
    /// </summary>
    public interface IEntityConverter
    {
        /// <summary>
        /// Assigns properties of one object to another by registered mapping configuration.
        /// </summary>
        /// <typeparam name="TIn">Type of object to be assigned from.</typeparam>
        /// <typeparam name="TOut">Type of object to be assigned to.</typeparam>
        /// <param name="source">Object to be assigned from.</param>
        /// <param name="destination">Object to be assigned to.</param>
        void AssignTo<TIn, TOut>(TIn source, ref TOut destination);

        /// <summary>
        /// Converts passed object to the specified type.
        /// </summary>
        /// <typeparam name="TIn">Type of object to be converted from.</typeparam>
        /// <typeparam name="TOut">Type of the output object.</typeparam>
        /// <param name="source">Object to be converted from.</param>
        /// <returns>Converted object.</returns>
        TOut ConvertTo<TIn, TOut>(TIn source);

        void Merge<TIn, TOut>(TIn source, TOut destination);

        /// <summary>
        /// Deep copy of the object
        /// </summary>
        /// <typeparam name="T">A type of the object</typeparam>
        /// <param name="source">The source object</param>
        /// <returns>A copy of the source object</returns>
        T Copy<T>(T source);
    }
}
