using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;
using Serilog;

namespace CoffeeMapServer.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IRoasterTagRepository _roasterTagRepository;
        private readonly ILogger _logger;

        public TagService(ITagRepository tagRepository,
                          IRoasterTagRepository roasterTagRepository,
                          ILogger logger)
        {
            _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
            _roasterTagRepository = roasterTagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> DeleteTagAsync(Guid id)
        {
            try
            {
                _logger.Information("Tag service layer access in progress...");

                var removablePairs = await _roasterTagRepository.GetPairsByTagIdAsync(id);
                if (removablePairs.Count > 0)
                    _roasterTagRepository.DeleteRoasterTags(removablePairs);

                _tagRepository.Delete(await _tagRepository.GetSingleAsync(id));

                await _tagRepository.SaveChangesAsync();

                _logger.Information($"Tags table has been modified. Deleted tag:\n Id:{id}");
                return 0;

            }
            catch (Exception e)
            {
                _logger.Error($"Tag service layer error occured! Error text message: {e.Message}");
                return -1;
            }
        }

        public async Task<Tag> FetchSingleTagAsync(Guid id)
            => await _tagRepository.GetSingleAsync(id);

        public async Task<IList<Tag>> FetchTagsListAsync()
            => await _tagRepository.GetListAsync();

        public async Task<int> UpdateSingleTagAsync(Tag tag)
        {
            try
            {
                _logger.Information("Tag service layer access in progress...");
                var buffTag =await _tagRepository.GetSingleAsNoTrackingAsync(tag.TagTitle);
                if (buffTag != null && !buffTag.Id.Equals(tag.Id))
                    return -1;
                _tagRepository.Update(tag);
                await _tagRepository.SaveChangesAsync();

                _logger.Information($"Tags table has been modified. Updated tag:\n Id:{tag.Id}");
                return 0;
            }
            catch (Exception e)
            {
                _logger.Error($"Tag service layer error occured! Error text message: {e.Message}");
                return -2;
            }
        }
    }
}