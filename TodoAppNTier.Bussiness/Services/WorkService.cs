using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TodoAppNtier.DataAccess.UnitOfWork;
using TodoAppNTier.Bussiness.Interfaces;
using TodoAppNTier.Bussiness.ValidationRules;
using TodoAppNTier.Common.ResponseObjects;
using TodoAppNTier.Dtos.Interfaces;
using TodoAppNTier.Dtos.WorkDtos;
using TodoAppNTier.Entities.Domains;

namespace TodoAppNTier.Bussiness.Services
{
    public class WorkService : IWorkService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<WorkCreateDto> _createDtoValidator;
        private readonly IValidator<WorkUpdateDto> _updateDtoValidator;

        public WorkService(IUow uow, IMapper mapper, IValidator<WorkCreateDto> createDtoValidator, IValidator<WorkUpdateDto> updateDtoValidator)
        {
            _uow = uow;
            _mapper = mapper;
            _createDtoValidator = createDtoValidator;
            _updateDtoValidator = updateDtoValidator;
        }

        public async Task<IResponse<WorkCreateDto>> Create(WorkCreateDto dto)
        {
            var validationResult = _createDtoValidator.Validate(dto);

            if (validationResult.IsValid)
            {
                await _uow.GetRepository<Work>().Create(_mapper.Map<Work>(dto));
                await _uow.SaveChanges();
                return new Response<WorkCreateDto>(ResponseType.Success, dto);
            }
            else
            {
                List<CustomValidationError> errors = new();
                foreach (var item in validationResult.Errors)
                {
                    errors.Add(new()
                    {
                        ErrorMessage = item.ErrorMessage,
                        PropertyName = item.PropertyName
                    });
                }

                return new Response<WorkCreateDto>(ResponseType.ValidationError, dto, errors);
            }
        }

        public async Task<IResponse<List<WorkListDto>>> GetAll()
        {
            var data = _mapper.Map<List<WorkListDto>>(await _uow.GetRepository<Work>().GetAll());
            return new Response<List<WorkListDto>>(ResponseType.Success, data);
        }

        public async Task<IResponse<IDto>> GetById<IDto>(int id)
        {
            var data = _mapper.Map<IDto>(await _uow.GetRepository<Work>().GetByFilter(x => x.Id == id));

            if (data is null)
                return new Response<IDto>(ResponseType.NotFound, $"{id} ye ait data bulunamadı");
            return new Response<IDto>(ResponseType.Success, data);
        }

        public async Task<IResponse> Remove(int id)
        {
            var removedEntity = await _uow.GetRepository<Work>().GetByFilter(x => x.Id == id);

            if (removedEntity is not null)
            {
                _uow.GetRepository<Work>().Remove(removedEntity);
                await _uow.SaveChanges();
                return new Response(ResponseType.Success);
            }
            return new Response(ResponseType.NotFound, $"{id} ye ait data bulunamadı");
        }

        public async Task<IResponse<WorkUpdateDto>> Update(WorkUpdateDto dto)
        {
            var result = _updateDtoValidator.Validate(dto);
            if (result.IsValid)
            {
                var updatedEntity = await _uow.GetRepository<Work>().Find(dto.Id);
                if (updatedEntity is not null)
                {
                    _uow.GetRepository<Work>().Update(_mapper.Map<Work>(dto), updatedEntity);
                    await _uow.SaveChanges();
                    return new Response<WorkUpdateDto>(ResponseType.Success, dto);
                }
                return new Response<WorkUpdateDto>(ResponseType.NotFound, $"{dto.Id} ye ait data bulunamadı");
            }
            else
            {
                List<CustomValidationError> errors = new();
                foreach (var item in result.Errors)
                {
                    errors.Add(new()
                    {
                        ErrorMessage = item.ErrorMessage,
                        PropertyName = item.PropertyName
                    });
                }

                return new Response<WorkUpdateDto>(ResponseType.ValidationError, dto, errors);
            }

        }
    }
}
