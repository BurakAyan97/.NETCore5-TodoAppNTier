using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoAppNTier.Common.ResponseObjects;
using TodoAppNTier.Dtos.Interfaces;
using TodoAppNTier.Dtos.WorkDtos;

namespace TodoAppNTier.Bussiness.Interfaces
{
    public interface IWorkService
    {
        Task<IResponse<List<WorkListDto>>> GetAll();
        Task<IResponse<WorkCreateDto>> Create(WorkCreateDto dto);
        //Gruplama için bütün dtolara boş bir IDto interfacesi oluşturduk
        Task<IResponse<IDto>> GetById<IDto>(int id);
        Task<IResponse> Remove(int id);
        Task<IResponse<WorkUpdateDto>> Update(WorkUpdateDto dto);
    }
}
