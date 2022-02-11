using AutoMapper;
using Bogus.DataSets;
using EscNet.Cryptography.Interfaces;
using FluentAssertions;
using Manager.Domain.Entities;
using Manager.Infra.Interfaces;
using Manager.Services.DTO;
using Manager.Services.Interfaces;
using Manager.Services.Services;
using Manager.Tests.Configurations;
using Manager.Tests.Fixtures;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Manager.Tests.Projects.Services
{
    public class UserServiceTests
    {
        //Subject Under Test
        private readonly IUserService _sut;

        // Mocks
        private readonly IMapper _mapper;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IRijndaelCryptography> _rijndaelCryptographyMock;

        public UserServiceTests()
        {
            _mapper = AutoMapperConfiguration.GetConfiguration();
            _userRepositoryMock = new Mock<IUserRepository>();
            _rijndaelCryptographyMock = new Mock<IRijndaelCryptography>();

            _sut = new UserService(
                mapper: _mapper,
                userRepository: _userRepositoryMock.Object,
                rijndaelCryptography: _rijndaelCryptographyMock.Object
                );
        }

        // NOMEMETODO_CONDICAO_RESULTADOESPERADO
        [Fact(DisplayName = "Create valid user")]
        [Trait("Category", "Services")]
        public async Task Create_WhenUserIsValid_ReturnUserDTO()
        {
            // Arrange
            //var userToCreate = new UserDTO { Name = "Elton", Email = "elton@eu.com", Password = "123456987985" };
            var userToCreate = UserFixture.CreateValidUserDTO();

            var encryptedPassword = new Lorem().Sentence();
            var userCreated = _mapper.Map<User>(userToCreate);
            userCreated.ChangePassword(encryptedPassword);

            _userRepositoryMock.Setup(x => x.GetByEmail(It.IsAny<string>()))
                .ReturnsAsync(() => null);

            _rijndaelCryptographyMock.Setup(x => x.Encrypt(It.IsAny<string>()))
                .Returns(encryptedPassword);

            _userRepositoryMock.Setup(x => x.Create(It.IsAny<User>()))
                .ReturnsAsync(() => userCreated);

            // Act
            var result = await _sut.Create(userToCreate);

            // Assert
            result.Should()
                .BeEquivalentTo(_mapper.Map<UserDTO>(userCreated));
            //Assert.Equal(result, userToCreate);
        }
    }
}
