using AutoMapper;
using MyIhsan.Common;
using MyIhsan.Common.Core;
using MyIhsan.Service.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyIhsan.Service.Core
{

    public class BaseService<T, TDto> : IBaseService<T, TDto>
       where T : class
       where TDto : class
    {
        protected readonly IIdentityUnitOfWork<T> _unitOfWork;
        protected readonly IMapper Mapper;
        protected readonly IResponseResult ResponseResult;
        protected TDto currentModel { get; set; }
        protected IResponseResult result;
        protected internal BaseService(IServiceBaseParameter<T> businessBaseParameter)
        {
            _unitOfWork = businessBaseParameter.UnitOfWork;
            ResponseResult = businessBaseParameter.ResponseResult;
            Mapper = businessBaseParameter.Mapper;
        }
        public virtual async Task<IResponseResult> GetAllAsync()
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
        public virtual async Task<IResponseResult> AddAsync(TDto model)
        {
            try
            {
                PropertyInfo propertyInfo = model.GetType().GetProperty("Id");
                if (propertyInfo.PropertyType == typeof(string))
                {
                    propertyInfo.SetValue(model, Convert.ChangeType(Guid.NewGuid().ToString(), propertyInfo.PropertyType), null);
                }

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
        public virtual async Task<IResponseResult> UpdateAsync(TDto model)
        {
            try
            {
                var id = Helper.GetPropValue(model, "Id");
                T entityToUpdate = await _unitOfWork.Repository.GetAsync(id);
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
        public virtual async Task<IResponseResult> DeleteAsync(object id)
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
        public virtual async Task<IResponseResult> GetByIdAsync(object id)
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
