using AutoMapper;
using MyIhsan.Common.Core;
using MyIhsan.Identity.Service.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyIhsan.Identity.Service.Core
{

    public class BaseService<T, TDto> : IBaseService<T,TDto>
       where T : class
       where TDto : IPrimaryKeyField<string>
    {
        protected readonly IIdentityUnitOfWork<T> _unitOfWork;
        protected readonly IMapper Mapper;
        protected readonly IResponseResult ResponseResult;
        protected TDto currentModel { get; set; }
        protected const string AdmistratorId = "c21c91c0-5c2f-45cc-ab6d-1d256538a4ee";
        protected const string AdmistratorRoleId = "c21c91c0-5c2f-45cc-ab6d-1d256538a5ee";
        protected IResponseResult result;
        protected internal BaseService(IServiceBaseParameter<T> businessBaseParameter)
        {
            _unitOfWork = businessBaseParameter.UnitOfWork;
            ResponseResult = businessBaseParameter.ResponseResult;
            Mapper = businessBaseParameter.Mapper;
        }
        public async Task<IResponseResult> GetAllAsync()
        {
            try
            {
                var query = await _unitOfWork.Repository.GetAllAsync();
                var data = Mapper.Map<IEnumerable<T>, IEnumerable<TDto>>(query);
                return ResponseResult.GetRepositoryActionResult(data, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
            }
            catch (Exception e)
            {
                result.Message = e.InnerException != null ? e.InnerException.Message : e.Message;
                result = new ResponseResult(null, status: HttpStatusCode.InternalServerError, exception: e, message: result.Message);
                return result;
            }
        }
        public async Task<IResponseResult> AddAsync(TDto model)
        {
            try
            {
                model.Id = Guid.NewGuid().ToString();
                T entity = Mapper.Map<TDto, T>(model);
                _unitOfWork.Repository.Add(entity);
                int affectedRows = await _unitOfWork.SaveChanges();
                if (affectedRows > 0)
                {
                    result = new ResponseResult(result: null, status: HttpStatusCode.Created, message: "Data Inserted Successfully");
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
        public async Task<IResponseResult> UpdateAsync(TDto model)
        {
            try
            {
                T entityToUpdate = await _unitOfWork.Repository.GetAsync(model.Id);
                Mapper.Map(model, entityToUpdate);
                int affectedRows = await _unitOfWork.SaveChanges();
                if (affectedRows > 0)
                {
                    result = ResponseResult.GetRepositoryActionResult(result: true, status: HttpStatusCode.Accepted, message: "Data Updated Successfully");
                }

                return result;
            }
            catch (Exception e)
            {
                result.Message = e.InnerException != null ? e.InnerException.Message : e.Message;
                result = new ResponseResult(null, HttpStatusCode.InternalServerError, e, result.Message);
                return result;
            }
        }
        public async Task<IResponseResult> DeleteAsync(string id)
        {
            try
            {
                var entityToDelete = await _unitOfWork.Repository.GetAsync(id);
                _unitOfWork.Repository.Remove(entityToDelete);
                int affectedRows = await _unitOfWork.SaveChanges();
                if (affectedRows > 0)
                {
                    result = ResponseResult.GetRepositoryActionResult(result: true, status: HttpStatusCode.Accepted, message: "Data Updated Successfully");
                }
                return result;
            }
            catch (Exception e)
            {
                result.Message = e.InnerException != null ? e.InnerException.Message : e.Message;
                result = new ResponseResult(null, HttpStatusCode.InternalServerError, e, result.Message);
                return result;
            }
        }
        public async Task<IResponseResult> GetByIdAsync(string id)
        {
            try
            {
                T query = await _unitOfWork.Repository.GetAsync(id);
                var data = Mapper.Map<T, TDto>(query);
                return ResponseResult.GetRepositoryActionResult(result: data, status: HttpStatusCode.OK, message: "Data Updated Successfully");
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
