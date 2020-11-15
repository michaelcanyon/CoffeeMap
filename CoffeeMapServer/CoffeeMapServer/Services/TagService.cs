using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;

namespace CoffeeMapServer.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IRoasterTagRepository _roasterTagRepository;

        public TagService(
            ITagRepository tagRepository,
            IRoasterTagRepository roasterTagRepository)
        {
            _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
            _roasterTagRepository = roasterTagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
        }

        public async Task DeleteTagAsync(Guid id)
        {
            var removablePairs = await _roasterTagRepository.GetPairsByTagIdAsync(id);
            if (removablePairs.Count > 0)
                _roasterTagRepository.DeleteRoasterTags(removablePairs);

            _tagRepository.Delete(await _tagRepository.GetSingleAsync(id));

            await _tagRepository.SaveChangesAsync();
            await _roasterTagRepository.SaveChangesAsync();
        }

        public async Task<Tag> FetchSingleTagAsync(Guid id)
            => await _tagRepository.GetSingleAsync(id);

        public async Task<IList<Tag>> FetchTagsListAsync()
            => await _tagRepository.GetListAsync();

        public async Task UpdateSingleTagAsync(Tag tag)
        {
            _tagRepository.Update(tag);
            await _tagRepository.SaveChangesAsync();
        }
    }
}