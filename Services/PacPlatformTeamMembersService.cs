using Grpc.Core;
using ProtoBuf.Grpc;
using Google.Protobuf.WellKnownTypes;
using System.Linq;
using System.Threading.Tasks;

namespace POC.AutomatedTesting.gRPC.Services
{
    public class PacPlatformTeamMembersService : PacPlatformTeamMemberService.PacPlatformTeamMemberServiceBase
    {
        private static List<TeamMember> _teamMembers = new List<TeamMember>()
        {
            new TeamMember() { Id = 1, Name = "Gertjan", Email = "aaa@paclp.com", Role = "Team Leader1" },
            new TeamMember() { Id = 2, Name = "Youngho", Email = "young@paclp.com", Role = "Team Leader2" },
            new TeamMember() { Id = 3, Name = "Jesvin",Email= "jes@paclp.com", Role = "Team Leader3" },
            new TeamMember() { Id = 4, Name = "Jaap", Email = "jap@htec.com", Role = "Team Leader4" },
            new TeamMember() { Id = 5, Name = "Irena", Email = "irena@htec.com", Role = "Team Leader5" },
            new TeamMember() { Id = 6, Name = "Miodrag", Email = "md@htec.com", Role = "Team Leader6" },
            new TeamMember() { Id = 7, Name = "Darko", Email = "dk@htec.com", Role = "Team Leader7" }
        };

        public PacPlatformTeamMembersService() { }

        public override Task<TeamMember> GetTeamMember(TeamMemberId request, ServerCallContext context)
        {
            TeamMember? teamMember = _teamMembers.Find(p => p.Id == request.Id);

            if (teamMember == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "TeamMember Id is required."));
            }

            return Task.FromResult(teamMember);
        }

        public override Task<TeamMembers> GetTeamMemberByRole(TeamMemberRole request, ServerCallContext context)
        {
            var teamMembers = new TeamMembers();

            var items = _teamMembers.Where(p => p.Role.Contains(request.RoleName));

            teamMembers.TeamMember.AddRange(items);

            return Task.FromResult(teamMembers);
        }

        public override Task<TeamMembers> GetAllCurrentTeamMembers(Empty request, ServerCallContext context)
        {
            var teamMembers = new TeamMembers();
            teamMembers.TeamMember.AddRange(_teamMembers);
            return Task.FromResult(teamMembers);
        }

        public override Task<TeamMember> AddTeamMember(TeamMember request, ServerCallContext context)
        {
            TeamMember? t = _teamMembers.Find(p => p.Id == request.Id);

            if (request == null || t == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "TeamMember id is required."));
            }

            _teamMembers.Add(request);

            return Task.FromResult(request);
        }

        public override Task<TeamMembers> DeleteTeamMember(TeamMemberId request, ServerCallContext context)
        {
            TeamMember? teamMember = _teamMembers.Find(p => p.Id == request.Id);

            if (teamMember == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "TeamMember Id is required."));
            }
            _teamMembers.Remove(teamMember);

            var teamMembers = new TeamMembers();
            teamMembers.TeamMember.AddRange(_teamMembers);
            return Task.FromResult(teamMembers);
        }

        public override Task<TeamMember> UpdateTeamMember(TeamMember request, ServerCallContext context)
        {
            TeamMember? teamMember = _teamMembers.Find(p => p.Id == request.Id);

            if (teamMember == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "TeamMember Id is required."));
            }

            teamMember.Id = request.Id;
            teamMember.Name = request.Name;
            teamMember.Role = request.Role;
            teamMember.Email = request.Email;

            return Task.FromResult(teamMember);
        }

    }
}
