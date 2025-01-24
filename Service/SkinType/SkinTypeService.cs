using AutoMapper;
using SWP391_BE.Dtos.SkinType;
using Repo;
using Microsoft.Extensions.Logging;
using SWP391_BE.Exceptions;

namespace Service.SkinType
{
    public class SkinTypeService : ISkinTypeService
    {
        private readonly ISkinTypeRepository _skinTypeRepository;
        private readonly ISkinRoutineRepository _skinRoutineRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<SkinTypeService> _logger;

        public SkinTypeService(
            ISkinTypeRepository skinTypeRepository,
            ISkinRoutineRepository skinRoutineRepository,
            IProductRepository productRepository,
            IMapper mapper,
            ILogger<SkinTypeService> logger)
        {
            _skinTypeRepository = skinTypeRepository;
            _skinRoutineRepository = skinRoutineRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<SkinTypeDto> GetSkinTypeByIdAsync(int id)
        {
            var skinType = await _skinTypeRepository.GetByIdAsync(id);
            return _mapper.Map<SkinTypeDto>(skinType);
        }

        public async Task<List<SkinTypeDto>> GetAllSkinTypesAsync()
        {
            var skinTypes = await _skinTypeRepository.GetAllAsync();
            return _mapper.Map<List<SkinTypeDto>>(skinTypes);
        }

        public async Task<SkinTypeDto> DetermineSkinTypeAsync(SkinAssessmentDto assessment)
        {
            try
            {
                _logger.LogInformation("Starting skin type determination with {AnswerCount} answers", 
                    assessment.Answers.Count);

                if (!assessment.Answers.Any())
                {
                    throw new InvalidAssessmentException("Cần có ít nhất một câu trả lời để xác định loại da");
                }

                var skinTypes = await _skinTypeRepository.GetAllAsync();
                if (!skinTypes.Any())
                {
                    throw new SkinCareException("Không tìm thấy dữ liệu về các loại da");
                }

                // Logic xác định loại da dựa trên câu trả lời
                var determinedSkinType = await DetermineSkinTypeFromAnswers(assessment.Answers);
                
                _logger.LogInformation("Determined skin type: {SkinTypeId} - {SkinTypeName}", 
                    determinedSkinType.Id, determinedSkinType.Name);

                return _mapper.Map<SkinTypeDto>(determinedSkinType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error determining skin type from assessment");
                throw;
            }
        }

        public async Task<SkinRoutineDto> GetSkinRoutineAsync(int skinTypeId)
        {
            try
            {
                var routine = await _skinRoutineRepository.GetBySkinTypeIdAsync(skinTypeId);
                if (routine == null)
                {
                    throw new SkinRoutineNotFoundException(skinTypeId);
                }
                return _mapper.Map<SkinRoutineDto>(routine);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting skin routine for skin type {SkinTypeId}", skinTypeId);
                throw;
            }
        }

        public async Task<List<ProductDto>> GetRecommendedProductsForSkinTypeAsync(int skinTypeId)
        {
            var products = await _productRepository.GetBySkinTypeIdAsync(skinTypeId);
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
} 