using API.DTOs;
using API.Entities;
using API.Helper;
using API.Interface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.Users
                .Where(x=> x.UserName == username)
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams)
        {
            var qeury = _context.Users.AsQueryable();

            qeury = qeury.Where(u => u.UserName != userParams.CurrentUsername);
            qeury = qeury.Where(u => u.Gender == userParams.Gender);
            
            var minDob = DateOnly.FromDateTime(DateTime.Today.AddYears(-userParams.MaxAge - 1));
            var maxDob = DateOnly.FromDateTime(DateTime.Today.AddYears(-userParams.MinAge));

            qeury = qeury.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob);
            qeury = userParams.OrderBy switch
            {
                "created" => qeury.OrderByDescending(u => u.Created),
                _ => qeury.OrderByDescending(u => u.LastActive)
            };

            return await PagedList<MemberDto>.CreateAsync(
                qeury.AsNoTracking().ProjectTo<MemberDto>(_mapper.ConfigurationProvider), 
                userParams.PageNumber, 
                userParams.PageSize);
        }


        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(p => p.Photos)
                .SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users
                .Take(4)
                .Skip(4)
                .Include(p =>p.Photos)
                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}
