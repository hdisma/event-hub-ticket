using AutoMapper;
using EventHubTicket.Management.Application.Abstractions.Persistence;
using EventHubTicket.Management.Application.Features.Categories.Commands;
using EventHubTicket.Management.Application.Mappings;
using EventHubTicket.Management.Domain.Entities;
using Moq;

namespace EventHubTicket.Management.Application.Tests.Categories.Commands
{
    public class CreateCategoryCommandHandlerTests
    {
        private readonly Mock<IAsyncRepository<Category>> _categoryRepository;
        private readonly IMapper _mapper;

        private static List<Category> _categories = new();

        public CreateCategoryCommandHandlerTests()
        {
            _categoryRepository = new Mock<IAsyncRepository<Category>>();
            _mapper = new MapperConfiguration(config => config.AddProfile<MappingProfile>())
                .CreateMapper();

            InitializeCategories();
        }

        [Fact]
        public async Task Handle_WhenValidCreateCategoryCommand_ReturnsCreatedCategory()
        {
            _categoryRepository.Setup(cr => cr.CreateAsync(It.IsAny<Category>())).Returns((Category category) =>
            {
                _categories.Add(category);
                return Task.FromResult(category);
            });

            var createCategoryCommandHandler = new CreateCategoryCommandHandler(_categoryRepository.Object, _mapper);
            var createCategoryCommand = new CreateCategoryCommand { Name = "Musicals" };

            var result = await createCategoryCommandHandler.Handle(createCategoryCommand, CancellationToken.None);

            Assert.Multiple(
                () => Assert.Equal("Musicals", result.Category.Name),
                () => Assert.Equal(2, _categories.Count));
        }

        private IReadOnlyList<Category> InitializeCategories()
        {
            _categories.AddRange(new List<Category>()
            {
                new Category
                {
                    Id = Guid.Parse("8079d26a-1272-4ef5-9e9b-26d7ac4fdb7c"),
                    Name = "Concerts"
                }
            });

            return _categories;
        }
    }
}
