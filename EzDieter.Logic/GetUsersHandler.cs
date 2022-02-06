using EzDieter.Database;

namespace EzDieter.Logic
{
    public class GetUsersHandler
    {
        private readonly IUserRepository _userRepository;

        public GetUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        
    }
}