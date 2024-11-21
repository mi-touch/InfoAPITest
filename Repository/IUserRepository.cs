/// <summary>
/// Defines methods for managing User entities in the repository.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Retrieves all users.
    /// </summary>
    /// <returns>A collection of users.</returns>
    Task<IEnumerable<User>> Get();

    /// <summary>
    /// Retrieves a specific user by ID.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    /// <returns>The user with the specified ID.</returns>
    Task<User> Get(int id);

    /// <summary>
    /// Creates a new user in the repository.
    /// </summary>
    /// <param name="user">The user to create.</param>
    /// <returns>The created user.</returns>
    Task<User> Create(User user);

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="user">The user with updated details.</param>
    Task Update(User user);

    /// <summary>
    /// Deletes a user by ID.
    /// </summary>
    /// <param name="id">The ID of the user to delete.</param>
    Task Delete(int id);
}
