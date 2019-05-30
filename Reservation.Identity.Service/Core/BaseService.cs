using AutoMapper;
using Reservation.Common.Core;
using Reservation.Identity.Service.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Identity.Service.Core
{
   
    public class BaseService<T,TDto> :IBaseService<T,TDto>
        where T : class
        where TDto:class
    {
        protected readonly IIdentityUnitOfWork<T> _unitOfWork;
        protected readonly IMapper Mapper;
        protected readonly IResponseResult ResponseResult;
        protected TDto currentModel { get; set; }
        protected const string AdmistratorId = "c21c91c0-5c2f-45cc-ab6d-1d256538a4ee";
        protected const string AdmistratorRoleId = "c21c91c0-5c2f-45cc-ab6d-1d256538a5ee";
        protected IResponseResult result;
        protected internal BaseService(IBusinessBaseParameter<T> businessBaseParameter)
        {
            _unitOfWork = businessBaseParameter.UnitOfWork;
            ResponseResult = businessBaseParameter.ResponseResult;
            Mapper = businessBaseParameter.Mapper;
        }
        public async Task<IResponseResult> GetAllAsync()
        {
            var data = await _unitOfWork.Repository.GetAll();
            return ResponseResult.GetRepositoryActionResult(data, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString()); ;
        }
        public virtual async Task<IResponseResult> AddAsync(TDto model)
        {
            try
            {
                currentModel = model;
                T entity = Mapper.Map<TDto, T>(model);
                _unitOfWork.Repository.Add(entity);
                int affectedRows = await _unitOfWork.SaveChanges();
                if (affectedRows > 0)
                {
                    result = new ResponseResult(result: null, HttpStatusCode.Created, null, "Data Inserted Successfully");
                }

                result.Data = model;
                return result;
            }
            catch (Exception e)
            {
                result.Message = e.InnerException != null ? e.InnerException.Message : e.Message;
                result = new ResponseResult(null, HttpStatusCode.InternalServerError, e, result.Message);
                return result;
            }
        }

    }
}
